using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    [Header("아이템")]
    [SerializeField]
    private GameObject itemPrefab = null;
    [SerializeField]
    private GameObject coinPrefab = null;
    [SerializeField]
    private GameObject smallEnemy;

    [Header("번개")]
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

    [Header("배경")]
    [SerializeField]
    private GameObject cloud;
    [SerializeField]
    private GameObject cloud2;
    [SerializeField]
    private GameObject sun;
    [SerializeField]
    private GameObject dark;

    [SerializeField]
    private Text characterName, characterSpeech;
    [SerializeField]
    private GameObject textBox, shiba, stranger, sliderCheck, coinCheck, heartCheck;

    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }

    public int Life { get; private set; } = 3;

    public PoolManager poolManager { get; private set; }
    public EnemyPoolManager enemyPoolManager { get; private set; }
    public UIManager uiManager { get; private set; }
    public PlayerMove playerMove { get; private set; }
    public SoundManager soundManager { get; private set; }

    private string isFirst;
    private int cnt = 0;
    private bool isTutorial;
    private int lifeCount = 3;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if(instance == null)
                {
                    instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    void Start()
    {
        isFirst = PlayerPrefs.GetString("First", "true");
        if (isFirst == "true")
        {
            StartCoroutine(Tutorial());
        }

        else
        {
            Time.timeScale = 1;
        }

        enemyPoolManager = FindObjectOfType<EnemyPoolManager>();
        poolManager = FindObjectOfType<PoolManager>();
        uiManager = FindObjectOfType<UIManager>();
        playerMove = FindObjectOfType<PlayerMove>();
        soundManager = FindObjectOfType<SoundManager>();

        MinPosition = new Vector2(-9f, -4f);
        MaxPosition = new Vector2(9f, 4f);

        StartCoroutine(SpawnItem());
        StartCoroutine(SpawnCoin());
        StartCoroutine(SpawnCloud());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isFirst == "true")
        {
            uiManager.TutorialStop();
            return;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && PlayerPrefs.GetString("First") != "true")
        {
            uiManager.OnClickStop();
        }

        if (Input.GetMouseButtonDown(0) && textBox.activeSelf)
        {
            cnt++;

            TutorialText();
        }
    }

    public void Dead()
    {
        Life--;
        uiManager.ActiveHeart();
        uiManager.SetScore();

        if (Life <= 0)
        {
            if (uiManager.EnemyHP() == 160)
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

    public bool GetIsTutorial()
    {
        return isTutorial;
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

            uiManager.RandomItem(randomNum);
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

        if (uiManager.EnemyHP() == 160)
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
            uiManager.ActiveHeart();
        }
    }

    private IEnumerator Tutorial()
    {
        yield return new WaitForSeconds(2.0f);
        Time.timeScale = 0;
        isTutorial = false;
        textBox.SetActive(true);
        shiba.SetActive(false);
        stranger.SetActive(true);
        CharacterText("???", "잠깐!! 그 총알... 맞으면 엄청 아플지도 몰라!");
    }

    private IEnumerator Tutorial_Move()
    {
        yield return new WaitForSeconds(4f);
        Time.timeScale = 0;
        textBox.SetActive(true);
        shiba.SetActive(false);
        stranger.SetActive(true);
        CharacterText("???", "어때! 잘 움직여지지?");
    }

    private void CharacterText(string charName, string charText)
    {
        characterName.text = string.Format(charName);
        characterSpeech.text = string.Format(" ");
        characterSpeech.DOText(charText, 1f, true).SetUpdate(true);
    }

    private void TutorialText()
    {
        switch (cnt)
        {
            case 1:

                CharacterText("시바선생님", "총알...? 근데 누구세요...?");
                shiba.SetActive(true);
                stranger.SetActive(false);
                break;

            case 2:
                CharacterText("???", "알 거 없어!! 근데 너, 교장선생님의 명령으로 \n저 애를 유니티 만점이 되게 하려는 거지?");
                shiba.SetActive(false);
                stranger.SetActive(true);
                break;

            case 3:
                CharacterText("시바선생님", "네 맞긴 한데...");
                shiba.SetActive(true);
                stranger.SetActive(false);
                break;

            case 4:
                CharacterText("???", "너 정말! 지금 저 애는 유니티가 너무 미워서 정신이 이상해졌단 말이야!");
                shiba.SetActive(false);
                stranger.SetActive(true);
                break;

            case 5:
                CharacterText("???", "무진장 폭력적이라고!");
                break;

            case 6:
                CharacterText("???", "아무것도 몰라서는, 저 애가 던지는 총알을 요리조리 피하면서 유니티를 가르칠 수 있을 것 같아?");
                break;

            case 7:
                CharacterText("???", "어쩔 수 없지...");
                break;

            case 8:
                CharacterText("???", "내가 이제부터 보여줄테니까! 똑똑히 따라오라고");
                break;

            case 9:
                CharacterText("???", "먼저! 화면을 드래그해서 요리조리 움직여봐!");
                break;

            case 10:
                Time.timeScale = 1f;
                textBox.SetActive(false);
                isTutorial = true;
                StartCoroutine(Tutorial_Move());
                break;

            case 11:
                CharacterText("???", "네가 쏘고 있는 유니티 총알을 이용해서 학생에게 유니티를 가르치면 되고");
                break;

            case 12:
                sliderCheck.SetActive(true);
                CharacterText("???", "학생의 유니티 게이지를 끝까지 올리면 되는 거야.");
                break;

            case 13:
                sliderCheck.SetActive(false);
                CharacterText("???", "계속 아이템이 나오기도 하는데");
                break;

            case 14:
                CharacterText("???", "네가 직접 유니티를 가르치면서 아이템 효과를 확인해봐");
                break;

            case 15:
                CharacterText("???", "좋은 아이템도, 나쁜 아이템도 있을 거야.");
                break;

            case 16:
                coinCheck.SetActive(true);
                CharacterText("???", "그리고 저 코인을 모아서 아이템을 업그레이드 하거나");
                break;

            case 17:
                CharacterText("???", "예쁜 옷을 살 수도 있지!");
                break;

            case 18:
                coinCheck.SetActive(false);
                CharacterText("???", "내가 너무 가르쳐준 게 없을지도 모르지만...");
                break;

            case 19:
                CharacterText("???", "모쪼록 잘 해결해봐! 저 친구가 유니티 장인이 될 수 있도록 말이야!");
                break;

            case 20:
                sliderCheck.SetActive(true);
                CharacterText("???", "아! 학생의 유니티 게이지가 높아질수록, 더 난폭해지니까 주의해!");
                break;

            case 21:
                sliderCheck.SetActive(true);
                CharacterText("???", "특히 저 게이지가 다 차면 악마가 되니까 조심해!");
                break;

            case 22:
                sliderCheck.SetActive(true);
                CharacterText("???", "학생이 악마가 되면 총알의 움직임이 불규칙적이기도 하고");
                break;

            case 23:
                CharacterText("???", "갑자기 안개가 끼는 등 어려움이 많단 말이야.");
                break;

            case 24:
                sliderCheck.SetActive(false);
                heartCheck.SetActive(true);
                CharacterText("???", "학생의 총알을 맞으면 저 위에 생명 하나가 줄어드는데...");
                break;

            case 25:
                CharacterText("???", "하트가 모두 없어지지 않도록 조심하는 게 좋을거야.");
                break;

            case 26:
                heartCheck.SetActive(false);
                CharacterText("???", "그리고 나중에는 학생이 구름을 떨어뜨릴 수도 있으니까 그것도 조심해야해!");
                break;

            case 27:
                CharacterText("???", "그럼, 행운을 빌어 시바 선생!");
                break;

            case 28:
                textBox.SetActive(false);
                PlayerPrefs.SetString("First", "false");
                isFirst = PlayerPrefs.GetString("First");
                isTutorial = false;
                Time.timeScale = 1;
                soundManager.EndTutorial();
                Life = 3;
                uiManager.ActiveHeart();
                break;
        }
    }

    public void SetThreeHeart()
    {
        lifeCount = 5;
        Life = 5;
        uiManager.ActiveHeart();
    }

    public IEnumerator RealBossTime()
    {
        Time.timeScale = 0.4f;
        yield return new WaitForSeconds(1.3f);
        Time.timeScale = 1f;
    }

    public bool ReturnIsTutorial()
    {
        return isTutorial;
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
}