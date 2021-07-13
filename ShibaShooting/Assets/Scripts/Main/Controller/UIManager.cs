using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("생명")]
    [SerializeField] private GameObject hearts;

    [Header("게임 중지 팝업")]
    [SerializeField] private GameObject stopPopUp;
    [SerializeField] private GameObject musicPopUp;
    [SerializeField] private GameObject tutorialPopup;


    [Header("딜레이 타임 텍스트")]
    [SerializeField] private Text textDelayTime;
    [SerializeField] private GameObject textDelayTimeObj;

    [Header("딜레이 타임 텍스트")]
    [SerializeField]
    private Slider enemyHPBar;

    [Header("점수")]
    [SerializeField]
    private Text textScore = null;
    [SerializeField]
    private Text textHighScore = null;
    [SerializeField]
    private Text textCoin = null;

    [Header("아이템 스프라이트")]
    [SerializeField] private Sprite itemBig;
    [SerializeField] private Sprite itemSlow;
    [SerializeField] private Sprite itemCoin;
    [SerializeField] private Sprite itemLightning;
    [SerializeField] private Sprite itemHeart;
    [SerializeField] private Sprite itemSmall;
    [SerializeField] private Sprite itemTired;
    [SerializeField] private SpriteRenderer item;

    [Header("음악")]
    [SerializeField]
    private AudioSource music;

    private int score = 0;
    private int highScore = 0;
    private int coin = 0;
    private int countTime = 3;

    private bool isStop = false;

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HIGHSCORE", 500);
        coin = PlayerPrefs.GetInt("COIN", 0);
        PlayerPrefs.SetInt("Score", 0);

        UpdateUI();
    }

    public int ReturnScore()
    {
        return score;
    }

    public void ActiveHeart()
    {
        if (GameManager.Instance.Life == 5)
        {
            for (int i = 1; i <= 5; i++)
            {
                hearts.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        else if (GameManager.Instance.Life == 4)
        {
            for (int i = 1; i <= 4; i++)
            {
                hearts.transform.GetChild(i).gameObject.SetActive(true);
            }

            hearts.transform.GetChild(5).gameObject.SetActive(false);
        }

        else if (GameManager.Instance.Life == 3)
        {
            for (int i = 1; i <= 3; i++)
            {
                hearts.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 5; i >= 4; i--)
            {
                hearts.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        else if (GameManager.Instance.Life == 2)
        {
            for (int i = 1; i <= 2; i++)
            {
                hearts.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 5; i >= 3; i--)
            {
                hearts.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        else if (GameManager.Instance.Life == 1)
        {
            hearts.transform.GetChild(1).gameObject.SetActive(true);

            for (int i = 5; i >= 2; i--)
            {
                hearts.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
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

    public void TutorialStop()
    {
        tutorialPopup.SetActive(true);
    }

    public void TutorialNonstop()
    {
        tutorialPopup.SetActive(false);
    }

    public void AddCoin(int addCoin)
    {
        this.coin += addCoin;
        PlayerPrefs.SetInt("COIN", coin);
        UpdateUI();
    }

    public void OnClickStop()
    {
        if (isStop) return;
        isStop = true;
        GameManager.Instance.soundManager.ButtonAudio();
        GameManager.Instance.StopGame();
        stopPopUp.SetActive(true);
        textDelayTimeObj.SetActive(false);
    }

    public bool GetIsStop()
    {
        return isStop;
    }

    public void OnClickMusic()
    {
        if (isStop) return;
        isStop = true;
        GameManager.Instance.soundManager.ButtonAudio();
        GameManager.Instance.StopGame();
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
        isStop = false;
        ButtonSound();
        stopPopUp.SetActive(false);
        SceneManager.LoadScene("Main");
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
        GameManager.Instance.soundManager.ButtonAudio();
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

        GameManager.Instance.ContinueGame();
        isStop = false;
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

            case 7:
                item.sprite = itemTired;
                break;
        }
    }

    public void SetScore()
    {
        PlayerPrefs.SetInt("Score", score);
    }
}
