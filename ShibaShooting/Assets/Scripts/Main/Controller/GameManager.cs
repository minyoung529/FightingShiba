using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private User user;
    public User CurrentUser { get { return user; } }

    [SerializeField] private List<ItemBase> items = new List<ItemBase>();

    #region InGame
    [Header("æ∆¿Ã≈€")]
    [SerializeField] private GameObject itemPrefab = null;
    [SerializeField] private GameObject coinPrefab = null;
    [SerializeField] private GameObject smallEnemy;

    [Header("πË∞Ê")]
    [SerializeField] private GameObject[] clouds;
    [SerializeField] private GameObject sun;
    [SerializeField] private GameObject dark;

    public CharacterBase player { get; private set; }

    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }
    #endregion

    #region Controller
    public PoolManager poolManager;
    public UIManager UIManager { get; private set; }
    public TutorialManager tutorialManager { get; private set; }
    #endregion

    #region Data
    private string SAVE_PATH = "";
    private readonly string SAVE_FILENAME = "/SaveFile.txt";
    #endregion

    private int lifeCount = 3;
    public int Life { get; private set; } = 3;
    public bool isGameOver = false;

    public CameraMove mainCam;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        CreateSaveFile();
        LoadFromJson();
    }

    void Start()
    {
        SceneManager.LoadScene(ConstantManager.UI_SCENE, LoadSceneMode.Additive);
        FindControllerObjects();

        Time.timeScale = 1;

        MinPosition = new Vector2(-9f, -4f);
        MaxPosition = new Vector2(9f, 4f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && user.GetIsCompleteTutorial())
        {
            UIManager.InactiveTutorial();
            return;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && !user.GetIsCompleteTutorial())
        {
            UIManager.OnClickStop();
        }
    }

    #region User_Data_Save
    private void CreateSaveFile()
    {
        SAVE_PATH = Application.dataPath + "/Save";

        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
    }

    private void LoadFromJson()
    {
        string json;

        if (File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
        }

        else
        {
            SaveToJson();
            LoadFromJson();
        }
    }

    private void SaveToJson()
    {
        SAVE_PATH = Application.dataPath + "/Save";

        if (user == null) return;
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }

    #endregion

    public void FindControllerObjects()
    {
        poolManager ??= FindObjectOfType<PoolManager>();
        UIManager ??= FindObjectOfType<UIManager>();
        player ??= FindObjectOfType<CharacterBase>();
    }
    public void GameStart()
    {
        FindControllerObjects();

        StartCoroutine(SpawnItem());
        StartCoroutine(SpawnCoin());
        StartCoroutine(SpawnCloud());
    }

    public void Dead()
    {
        Life--;
        UIManager.ActiveHeart();

        if (Life <= 0)
        {
            if (UIManager.GetEnemyHP() == 160)
            {
                PlayerPrefs.SetString("GameOver", "false");
            }

            else
            {
                PlayerPrefs.SetString("GameOver", "true");
            }

            //SceneManager.LoadScene("GameOver");
        }
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    private IEnumerator SpawnItem()
    {
        float randomY = 0f;
        float randomDelay = 0f;

        while (!isGameOver)
        {
            randomY = Random.Range(-3.5f, 3.5f);
            randomDelay = Random.Range(6f, 7f);

            yield return new WaitForSeconds(randomDelay);

            if (isGameOver) yield break;

            GameObject obj = Instantiate(itemPrefab, new Vector2(12f, randomY), Quaternion.identity);
            obj.GetComponent<Item>().SetItem(items[Random.Range(0, items.Count)]);
        }
    }

    private IEnumerator SpawnCoin()
    {
        float randomY = 0f;
        float randomDelay = 0f;

        while (!isGameOver)
        {
            yield return new WaitForSeconds(randomDelay);

            randomY = Random.Range(-3.5f, 3.5f);
            randomDelay = Random.Range(7f, 1f);
            if (isGameOver) yield break;
            if (poolManager.IsInPoolObject("Coin"))
            {
                GameObject obj = poolManager.GetPoolObject("Coin");
                obj.SetActive(true);
                obj.transform.position = new Vector2(12f, randomY);
            }
            else
            {
                Instantiate(coinPrefab, new Vector2(12f, randomY), Quaternion.identity);
            }
        }
    }

    private IEnumerator SpawnSmallEnemy()
    {
        float randomY = 0f;

        randomY = Random.Range(-2.5f, 2.5f);

        Instantiate(smallEnemy, new Vector2(7f, randomY), Quaternion.identity);

        if (UIManager.GetEnemyHP() == 160)
        {
            while (true)
            {
                Instantiate(smallEnemy, new Vector2(7f, randomY), Quaternion.identity);
                Instantiate(smallEnemy, new Vector2(7f, randomY), Quaternion.identity);

                yield return new WaitForSeconds(10f);
            }
        }

        yield break;
    }

    private IEnumerator SpawnCloud()
    {
        float randomY = 0f;
        float randomDelay = 0f;

        randomY = Random.Range(2f, 4f);
        randomDelay = Random.Range(5f, 7f);

        Instantiate(clouds[0], new Vector2(12f, randomY - 0.2f), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(clouds[1], new Vector2(12f, randomY), Quaternion.identity);
        yield return new WaitForSeconds(randomDelay);
        Instantiate(sun, new Vector2(12f, randomY), Quaternion.identity);
        yield return new WaitForSeconds(randomDelay);
    }

    public void PlusHeart(int number)
    {
        if (Life < lifeCount)
        {
            Life += number;
            UIManager.ActiveHeart();
        }
    }

    public void SetLifeCount(int life)
    {
        lifeCount = life;
        Life = life;
        UIManager.ActiveHeart();
    }

    public IEnumerator RealBossTime()
    {
        Time.timeScale = 0.4f;
        yield return new WaitForSeconds(1.3f);
        Time.timeScale = 1f;
    }

    public IEnumerator DarkActive()
    {
        SpriteRenderer darkRenderer;
        darkRenderer = dark.GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(10f);

        while (true)
        {
            darkRenderer.DOFade(1f, 0.5f);
            yield return new WaitForSeconds(2f);
            darkRenderer.DOFade(0f, 0.5f);
            yield return new WaitForSeconds(10f);
        }
    }

    public void SetLife(int life)
    {
        Life = life;
        UIManager.ActiveHeart();
    }

    // Set Character Type (Enum) from name (string)
    public void SetCharacterType(ref CharacterType characterType)
    {
        switch (UIManager.GetPreviousCharacterName())
        {
            case "Ω√πŸº±ª˝¥‘":
                characterType = CharacterType.Player;
                break;

            case "???":
                characterType = CharacterType.TutorialHelper;
                break;

            case "≤‹∞ı":
                characterType = CharacterType.StoreHelper;
                break;
        }
    }

    //HACK:
    public void SetPlayer(CharacterBase playerMove)
    {
        this.player = playerMove;
    }

    //HACK:
    public void SetTutorialManager(TutorialManager tutorialManager)
    {
        this.tutorialManager = tutorialManager;
    }

    public void SetPoolManager(PoolManager poolManager)
    {
        this.poolManager = poolManager;
    }

    private void OnApplicationQuit()
    {
        SaveToJson();
    }

    public List<ItemBase> GetPurchaseItems()
    {
        return items;
    }

    public void Vibrate()
    {
        if (CurrentUser.isVibrate)
        {
            Handheld.Vibrate();
        }
    }
}