using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("����")]
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;

    [Header("���� ���� �˾�")]
    [SerializeField] private GameObject stopPopUp;
    [SerializeField] private GameObject musicPopUp;

    [Header("������ Ÿ�� �ؽ�Ʈ")]
    [SerializeField] private Text textDelayTime;
    [SerializeField] private GameObject textDelayTimeObj;

    [Header("������ Ÿ�� �ؽ�Ʈ")]
    [SerializeField]
    private Slider enemyHPBar;

    [Header("����")]
    [SerializeField]
    private Text textScore = null;
    [SerializeField]
    private Text textHighScore = null;
    [SerializeField]
    private Text textCoin = null;

    [Header("������ ��������Ʈ")]
    [SerializeField] Sprite itemBig;
    [SerializeField] Sprite itemSlow;
    [SerializeField] Sprite itemCoin;
    [SerializeField] Sprite itemLightning;
    [SerializeField] Sprite itemHeart;
    [SerializeField] Sprite itemSmall;
    [SerializeField] SpriteRenderer item;

    [Header("����")]
    [SerializeField]
    private AudioSource music;

    private int score = 0;
    private int highScore = 0;
    private int coin = 0;
    private int countTime = 3;

    private GameManager gameManager = null;


    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HIGHSCORE", 500);
        coin = PlayerPrefs.GetInt("COIN", 0);
        gameManager = FindObjectOfType<GameManager>();

        UpdateUI();
    }

    public int ReturnScore()
    {
        int score_ = score;
        return score_;
    }
    public void DestroyHeart()
    {
        if (gameManager.Life == 2)
            heart3.SetActive(false);

        else if (gameManager.Life == 1)
            heart2.SetActive(false);

        else if (gameManager.Life == 0)
            heart1.SetActive(false);
    }

    public void ActiveHeart()
    {
        if (gameManager.Life == 3)
            heart3.SetActive(true);

        else if (gameManager.Life == 2)
            heart2.SetActive(true);

        else if (gameManager.Life == 1)
            heart1.SetActive(true);
    }

    public void UpdateUI()
    {
        textScore.text = string.Format("SCORE {0}", score);
        textHighScore.text = string.Format("HIGHSCORE {0}", highScore);
        textCoin.text = string.Format("{0}", coin);
    }

    public void AddScore(int addScore)
    {
        this.score += addScore;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HIGHSCORE", highScore);
        }
        UpdateUI();
    }

    public void AddCoin(int addCoin)
    {
        this.coin += addCoin;
        PlayerPrefs.SetInt("COIN", coin);
        UpdateUI();
    }

    public void OnClickStop()
    {
        gameManager.soundManager.ButtonAudio();
        gameManager.StopGame();
        stopPopUp.SetActive(true);
        textDelayTimeObj.SetActive(false);
    }

    public void OnClickMusic()
    {
        gameManager.soundManager.ButtonAudio();
        gameManager.StopGame();
        musicPopUp.SetActive(true);
        textDelayTimeObj.SetActive(false);
    }

    public void OnClickMenu()
    {
        ButtonSound();
        SceneManager.LoadScene("Lobby");
    }

    public void OnClickNewGame()
    {
        ButtonSound();
        stopPopUp.SetActive(false);
        SceneManager.LoadScene("Main");

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
        gameManager.soundManager.ButtonAudio();
    }

    private IEnumerator ContinueDelay()
    {
        textDelayTimeObj.SetActive(true);

        while (countTime > 0)
        {
            textDelayTime.text = string.Format("{0}", countTime);
            countTime--;
            yield return new WaitForSecondsRealtime(1f);

            if (countTime == 0)
                textDelayTime.text = string.Format("");
        }

        gameManager.ContinueGame();
        yield return 0;
    }

    public void EnemyHPBar(float damage)
    {
        enemyHPBar.value += damage;
    }

    public float EnemyHP()
    {
        return enemyHPBar.value;
    }


    public void RandomItem(int randomNum)
    {
        switch (randomNum)
        {
            case 1:
                item.sprite = itemBig;
                break;

            case 2:
                item.sprite = itemSlow;
                break;

            case 3:
                item.sprite = itemCoin;
                break;

            case 4:
                item.sprite = itemLightning;
                break;

            case 5:
                item.sprite = itemSmall;
                break;

            case 6:
                item.sprite = itemHeart;
                break;
        }
    }
}
