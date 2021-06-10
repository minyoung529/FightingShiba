using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("생명")]
    [SerializeField] private GameObject heart1;
    [SerializeField] private GameObject heart2;
    [SerializeField] private GameObject heart3;

    [Header("게임 중지 팝업")]
    [SerializeField] private GameObject stopPopUp;

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
    [SerializeField]
    Sprite itemBig;
    [SerializeField]
    Sprite itemSlow;
    [SerializeField]
    SpriteRenderer item;
    [SerializeField]
    Sprite itemLightning;

    [Header("음악")]
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

    public void DestroyHeart()
    {
        if (gameManager.life == 2)
            heart3.SetActive(false);

        else if (gameManager.life == 1)
            heart2.SetActive(false);

        else if (gameManager.life == 0)
            heart1.SetActive(false);
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
        gameManager.StopGame();
        stopPopUp.SetActive(true);
        textDelayTimeObj.SetActive(false);
        music.Stop();
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnClickNewGame()
    {
        stopPopUp.SetActive(false);
        SceneManager.LoadScene("Main");

    }

    public void OnClickContinue()
    {
        countTime = 3;
        stopPopUp.SetActive(false);
        StartCoroutine(ContinueDelay());
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
        music.Play();
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
                item.sprite = itemLightning;
                break;
        }
    }
}
