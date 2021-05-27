using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("점수")]
    [SerializeField]
    private Text textScore = null;
    [SerializeField]
    private Text textHighScore = null;

    [Header("배경 오브젝트")]
    [SerializeField]
    private GameObject cloud1 = null;
    [SerializeField]
    private GameObject cloud2 = null;
    [SerializeField]
    private GameObject yellowButterfly = null;

    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }

    private int score = 0;
    public int life { get; private set; } = 3;
    private int highScore = 0;

    public PoolManager poolManager{ get; private set; }
    private UIManager uiManager = null;

    private bool isStop = false;

    void Start()
    {
        poolManager = FindObjectOfType<PoolManager>();
        uiManager = FindObjectOfType<UIManager>();
        highScore = PlayerPrefs.GetInt("HIGHSCORE", 500);
        MinPosition = new Vector2(-9f, -4f);
        MaxPosition = new Vector2(9f, 4f);
       // UpdateUI();
    }

    public void UpdateUI()
    {
        textScore.text = string.Format("SCORE\n{0}", score);
        textHighScore.text = string.Format("HIGHSCORE\n{0}", highScore);
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

    public int GetLife()
    {
        return life;
    }

    public void Dead()
    {
        life--;
        //UpdateUI();
        uiManager.destroyHeart();
        if (life <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void StopGame()
    {
        isStop = true;
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        isStop = false;
        Time.timeScale = 1;
    }
}
