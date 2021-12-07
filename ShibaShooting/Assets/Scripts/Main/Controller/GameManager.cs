using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private User user;
    public User CurrentUser { get { return user; } }

    [Header("¾ÆÀÌÅÛ")]
    [SerializeField]
    private GameObject itemPrefab = null;
    [SerializeField]
    private GameObject coinPrefab = null;
    [SerializeField]
    private GameObject smallEnemy;

    [Header("¹ø°³")]
    [SerializeField]
    private GameObject lightningObj;
    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite lightningSprite;
    [SerializeField]
    private SpriteRenderer lightningRenderer;
    [SerializeField]
    private Collider2D lightningcol;

    [Header("¹è°æ")]
    [SerializeField]
    private GameObject cloud;
    [SerializeField]
    private GameObject cloud2;
    [SerializeField]
    private GameObject sun;
    [SerializeField]
    private GameObject dark;


    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }

    public int Life { get; private set; } = 3;

    public PoolManager poolManager { get; private set; }
    public UIManager UIManager { get; private set; }
    public PlayerMove playerMove { get; private set; }
    public TutorialManager tutorialManager { get; private set; }

    private string SAVE_PATH = "";
    private readonly string SAVE_FILENAME = "/SaveFile.txt";

    private int cnt = 0;
    private int lifeCount = 3;

    private void Awake()
    {
        CreateSaveFile();
        LoadFromJson();

        poolManager = FindObjectOfType<PoolManager>();
        UIManager = FindObjectOfType<UIManager>();
        playerMove = FindObjectOfType<PlayerMove>();
        tutorialManager = GetComponent<TutorialManager>();

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

    void Start()
    {
        Time.timeScale = 1;

        MinPosition = new Vector2(-9f, -4f);
        MaxPosition = new Vector2(9f, 4f);

        StartCoroutine(SpawnItem());
        StartCoroutine(SpawnCoin());
        StartCoroutine(SpawnCloud());
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

    public void Dead()
    {
        Life--;
        UIManager.ActiveHeart();
        UIManager.SetScore();

        if (Life <= 0)
        {
            if (UIManager.EnemyHP() == 160)
            {
                PlayerPrefs.SetString("GameOver", "false");
            }

            else
            {
                PlayerPrefs.SetString("GameOver", "true");
            }

            SceneManager.LoadScene("GameOver");
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
        int randomNum = 0;

        while (true)
        {
            randomY = Random.Range(-3.5f, 3.5f);
            randomDelay = Random.Range(6f, 7f);
            randomNum = Random.Range(0, 8);

            yield return new WaitForSeconds(randomDelay);

            UIManager.RandomItem(randomNum);
            Instantiate(itemPrefab, new Vector2(12f, randomY), Quaternion.identity);
        }
    }

    private IEnumerator SpawnCoin()
    {
        float randomY = 0f;
        float randomDelay = 0f;

        while (true)
        {
            randomY = Random.Range(-3.5f, 3.5f);
            randomDelay = Random.Range(7f, 1f);

            Instantiate(coinPrefab, new Vector2(12f, randomY), Quaternion.identity);
            yield return new WaitForSeconds(randomDelay);
        }
    }

    private IEnumerator SpawnSmallEnemy()
    {
        float randomY = 0f;

        randomY = Random.Range(-2.5f, 2.5f);

        Instantiate(smallEnemy, new Vector2(7f, randomY), Quaternion.identity);

        if (UIManager.EnemyHP() == 160)
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

        Instantiate(cloud, new Vector2(12f, randomY - 0.2f), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(cloud2, new Vector2(12f, randomY), Quaternion.identity);
        yield return new WaitForSeconds(randomDelay);
        Instantiate(sun, new Vector2(12f, randomY), Quaternion.identity);
        yield return new WaitForSeconds(randomDelay);
    }

    private IEnumerator SpawnLightning()
    {
        playerMove.DecoLightning(true);

        float randomX;

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);

            lightningcol.enabled = false;
            randomX = Random.Range(-8f, -3f);
            lightningObj.transform.position = new Vector2(randomX, 0f);
            lightningObj.SetActive(true);

            lightningRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            lightningRenderer.sprite = defaultSprite;

            yield return new WaitForSeconds(0.6f);

            lightningcol.enabled = true;
            lightningRenderer.sprite = lightningSprite;
            lightningRenderer.color = new Color(1f, 1f, 1f, 1f);

            yield return new WaitForSeconds(1f);

            lightningObj.SetActive(false);
            lightningcol.enabled = false;
        }

        playerMove.DecoLightning(false);
        playerMove.IsItem(false);
        yield break;
    }

    public void ItemHeart()
    {
        if (Life == lifeCount) return;

        if (Life < lifeCount)
        {
            Life++;
            UIManager.ActiveHeart();
        }
    }


    public void SetThreeHeart()
    {
        lifeCount = 5;
        Life = 5;
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

    public void SetCharacterType(ref CharacterType characterType)
    {
        switch (UIManager.GetPreviousCharacterName())
        {
            case "½Ã¹Ù¼±»ý´Ô":
                characterType = CharacterType.Player;
                break;

            case "???":
                characterType = CharacterType.TutorialHelper;
                break;

            case "²Ü°õ":
                characterType = CharacterType.StoreHelper;
                break;
        }
    }
}