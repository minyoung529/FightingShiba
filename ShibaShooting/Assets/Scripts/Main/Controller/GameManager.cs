using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoSingleton<GameManager>
{

    [Header("������")]
    [SerializeField]
    private GameObject itemPrefab = null;
    [SerializeField]
    private GameObject coinPrefab = null;
    [SerializeField]
    private GameObject smallEnemy;

    [Header("����")]
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

    [Header("���")]
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
        CharacterText("???", "���!! �� �Ѿ�... ������ ��û �������� ����!");
    }

    private IEnumerator Tutorial_Move()
    {
        yield return new WaitForSeconds(4f);
        Time.timeScale = 0;
        textBox.SetActive(true);
        shiba.SetActive(false);
        stranger.SetActive(true);
        CharacterText("???", "�! �� ����������?");
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

                CharacterText("�ùټ�����", "�Ѿ�...? �ٵ� ��������...?");
                shiba.SetActive(true);
                stranger.SetActive(false);
                break;

            case 2:
                CharacterText("???", "�� �� ����!! �ٵ� ��, ���弱������ ������� \n�� �ָ� ����Ƽ ������ �ǰ� �Ϸ��� ����?");
                shiba.SetActive(false);
                stranger.SetActive(true);
                break;

            case 3:
                CharacterText("�ùټ�����", "�� �±� �ѵ�...");
                shiba.SetActive(true);
                stranger.SetActive(false);
                break;

            case 4:
                CharacterText("???", "�� ����! ���� �� �ִ� ����Ƽ�� �ʹ� �̿��� ������ �̻������� ���̾�!");
                shiba.SetActive(false);
                stranger.SetActive(true);
                break;

            case 5:
                CharacterText("???", "������ �������̶��!");
                break;

            case 6:
                CharacterText("???", "�ƹ��͵� ���󼭴�, �� �ְ� ������ �Ѿ��� �丮���� ���ϸ鼭 ����Ƽ�� ����ĥ �� ���� �� ����?");
                break;

            case 7:
                CharacterText("???", "��¿ �� ����...");
                break;

            case 8:
                CharacterText("???", "���� �������� �������״ϱ�! �ȶ��� ��������");
                break;

            case 9:
                CharacterText("???", "����! ȭ���� �巡���ؼ� �丮���� ��������!");
                break;

            case 10:
                Time.timeScale = 1f;
                textBox.SetActive(false);
                isTutorial = true;
                StartCoroutine(Tutorial_Move());
                break;

            case 11:
                CharacterText("???", "�װ� ��� �ִ� ����Ƽ �Ѿ��� �̿��ؼ� �л����� ����Ƽ�� ����ġ�� �ǰ�");
                break;

            case 12:
                sliderCheck.SetActive(true);
                CharacterText("???", "�л��� ����Ƽ �������� ������ �ø��� �Ǵ� �ž�.");
                break;

            case 13:
                sliderCheck.SetActive(false);
                CharacterText("???", "��� �������� �����⵵ �ϴµ�");
                break;

            case 14:
                CharacterText("???", "�װ� ���� ����Ƽ�� ����ġ�鼭 ������ ȿ���� Ȯ���غ�");
                break;

            case 15:
                CharacterText("???", "���� �����۵�, ���� �����۵� ���� �ž�.");
                break;

            case 16:
                coinCheck.SetActive(true);
                CharacterText("???", "�׸��� �� ������ ��Ƽ� �������� ���׷��̵� �ϰų�");
                break;

            case 17:
                CharacterText("???", "���� ���� �� ���� ����!");
                break;

            case 18:
                coinCheck.SetActive(false);
                CharacterText("???", "���� �ʹ� �������� �� �������� ������...");
                break;

            case 19:
                CharacterText("???", "���ɷ� �� �ذ��غ�! �� ģ���� ����Ƽ ������ �� �� �ֵ��� ���̾�!");
                break;

            case 20:
                sliderCheck.SetActive(true);
                CharacterText("???", "��! �л��� ����Ƽ �������� ����������, �� ���������ϱ� ������!");
                break;

            case 21:
                sliderCheck.SetActive(true);
                CharacterText("???", "Ư�� �� �������� �� ���� �Ǹ��� �Ǵϱ� ������!");
                break;

            case 22:
                sliderCheck.SetActive(true);
                CharacterText("???", "�л��� �Ǹ��� �Ǹ� �Ѿ��� �������� �ұ�Ģ���̱⵵ �ϰ�");
                break;

            case 23:
                CharacterText("???", "���ڱ� �Ȱ��� ���� �� ������� ���� ���̾�.");
                break;

            case 24:
                sliderCheck.SetActive(false);
                heartCheck.SetActive(true);
                CharacterText("???", "�л��� �Ѿ��� ������ �� ���� ���� �ϳ��� �پ��µ�...");
                break;

            case 25:
                CharacterText("???", "��Ʈ�� ��� �������� �ʵ��� �����ϴ� �� �����ž�.");
                break;

            case 26:
                heartCheck.SetActive(false);
                CharacterText("???", "�׸��� ���߿��� �л��� ������ ����߸� ���� �����ϱ� �װ͵� �����ؾ���!");
                break;

            case 27:
                CharacterText("???", "�׷�, ����� ���� �ù� ����!");
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