using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("생명")]
    [SerializeField] private GameObject hearts;

    [Header("게임 중지 팝업")]
    [SerializeField] private GameObject stopPopUp;
    [SerializeField] private GameObject musicPopUp;
    [SerializeField] private GameObject tutorialPopup;
    [SerializeField] private GameObject quitPopup;

    [Header("Text")]
    [SerializeField] private GameObject warning;

    [Header("딜레이 타임 텍스트")]
    [SerializeField] private Text textDelayTime;

    [Header("hp")]
    [SerializeField] private Slider enemyHPBar;

    [Header("점수")]
    [SerializeField] private Text textScore = null;
    [SerializeField] private Text textHighScore = null;
    [SerializeField] private Text textCoin = null;

    [Header("아이템 스프라이트")]
    [SerializeField] private Sprite[] itemSprites;
    [SerializeField] private SpriteRenderer item;

    [Header("튜토리얼")]
    public Text characterName, characterSpeech;
    public GameObject textBox;
    public GameObject[] characters = new GameObject[(int)CharacterType.Count];

    [Header("캔버스")]
    [SerializeField] private Canvas[] canvases;

    [Header("StorePanel")]
    [SerializeField] private PurchasePanel purchasePanel;
    private List<PurchasePanel> purchasePanels = new List<PurchasePanel>();


    private int score = 0;
    private int highScore = 0;
    private int countTime = 3;

    private bool isStop = false;

    private void Start()
    {
        highScore = GameManager.Instance.CurrentUser.GetHighScore();
        UpdateUI();
        DontDestroyOnLoad(this);
        //InstantiatePanel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitPopup.gameObject.SetActive(!quitPopup.activeSelf);
        }
    }

    #region
    private void InstantiatePanel()
    {
        for (int i = 0; i < GameManager.Instance.GetPurchaseItems().Count; i++)
        {
            Instantiate(purchasePanel.gameObject, purchasePanel.transform.parent);
            PurchasePanel panel = purchasePanel.GetComponent<PurchasePanel>();
            purchasePanels.Add(panel);
            panel.Init(GameManager.Instance.GetPurchaseItems()[i]);
        }
    }
    #endregion
    public void ActiveHeart()
    {
        for (int i = 0; i < ConstantManager.PLAYER_FULL_LIFE; i++)
        {
            hearts.transform.GetChild(i).gameObject.SetActive(i < GameManager.Instance.Life);
        }
    }

    public void UpdateUI()
    {
        textScore.text = string.Format("SCORE {0}", score);
        textHighScore.text = string.Format("HIGHSCORE {0}", highScore);
        textCoin.text = string.Format("{0}", GameManager.Instance.CurrentUser.GetCoin());
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        if (score > highScore)
        {
            highScore = score;
            GameManager.Instance.CurrentUser.SetHighScore(highScore);
        }
        UpdateUI();
    }

    public void InactiveTutorial()
    {
        tutorialPopup.SetActive(true);
    }

    public void AddCoin(int addCoin)
    {
        GameManager.Instance.CurrentUser.AddCoin(addCoin);
        UpdateUI();
    }

    public void OnClickStop()
    {
        if (isStop) return;
        isStop = true;
        SoundManager.Instance.ButtonAudio();
        GameManager.Instance.StopGame();
        stopPopUp.SetActive(true);
        textDelayTime.gameObject.SetActive(false);
    }


    public void OnClickMusic()
    {
        if (isStop) return;
        isStop = true;
        SoundManager.Instance.ButtonAudio();
        GameManager.Instance.StopGame();
        musicPopUp.SetActive(true);
        textDelayTime.gameObject.SetActive(false);
    }

    public void OnClickMenu()
    {
        ButtonSound();
        GameManager.Instance.isGameOver = true;
        stopPopUp.SetActive(false);
        GoToScene(SceneType.Lobby);
    }

    public void OnClickNewGame()
    {
        isStop = false;
        ButtonSound();
        stopPopUp.SetActive(false);
        GoToScene(SceneType.Main);
        GameManager.Instance.ContinueGame();
    }

    public void OnClickContinue()
    {
        ButtonSound();
        countTime = 3;
        stopPopUp.SetActive(false);
        musicPopUp.SetActive(false);
        StartCoroutine(ContinueDelay());
    }

    private void ButtonSound()
    {
        SoundManager.Instance.ButtonAudio();
    }

    private IEnumerator ContinueDelay()
    {
        textDelayTime.gameObject.SetActive(true);

        while (countTime > 0)
        {
            textDelayTime.text = string.Format("{0}", countTime);
            countTime--;
            yield return new WaitForSecondsRealtime(1f);

            if (countTime == 0)
                textDelayTime.text = string.Format("");
        }

        GameManager.Instance.ContinueGame();
        isStop = false;
        yield return 0;
    }

    public void EnemyHPBar(float damage)
    {
        enemyHPBar.value += damage;
    }



    #region Tutorial
    public void CharacterText(string charName, string charText)
    {
        characterName.text = string.Format(charName);
        characterSpeech.text = "";
        characterSpeech.DOText(charText, 1f, true).SetUpdate(true);
    }

    public void ActiveCharacterInDialogue(int index)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(i == index);
        }
    }

    public void ActiveDialogueBox()
    {
        textBox.SetActive(!textBox.activeSelf);
    }

    public string GetPreviousCharacterName()
    {
        return characterName.text;
    }
    #endregion

    #region Lobby
    public void OnClickStart()
    {
        SoundManager.Instance.ButtonAudio();

        if (GameManager.Instance.CurrentUser.GetIsCompleteTutorial())
        {
            GoToScene(SceneType.Main);
            GameManager.Instance.GameStart();
        }

        else
        {
            GoToScene(SceneType.Story);
        }
    }
    #endregion

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToScene(SceneType sceneType)
    {
        SceneManager.LoadScene(sceneType.ToString());

        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].gameObject.SetActive(i == (int)sceneType);
        }

        GameManager.Instance.FindControllerObjects();
    }

    public void OnClickStorySkip()
    {
        GoToScene(SceneType.Main);
    }

    private IEnumerator Warning()
    {
        warning.SetActive(true);
        Text warn = warning.GetComponent<Text>();

        for (int i = 0; i < 5; i++)
        {
            warn.color = new Color(1, 0, 0, 1);
            yield return new WaitForSeconds(0.15f);
            warn.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.15f);
        }
        warning.SetActive(false);
    }
    public void StartWarning()
    {
        StartCoroutine(Warning());
    }

    public int GetScore()
    {
        return score;
    }

    public float GetEnemyHP()
    {
        return enemyHPBar.value;
    }

    public bool GetIsStop()
    {
        return isStop;
    }
}
