using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Text textScore = null;
    [SerializeField]
    private Text textLife = null;
    [SerializeField]
    private Text textHighScore = null;

    [SerializeField]
    private GameObject cloud1 = null;
    [SerializeField]
    private GameObject cloud2 = null;
    [SerializeField]
    private GameObject yellowButterfly = null;

    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }

    private int score = 0;
    private int life = 3;
    private int highScore = 0;

    public PoolManager poolManager{ get; private set; }

    void Start()
    {
        poolManager = FindObjectOfType<PoolManager>();
        highScore = PlayerPrefs.GetInt("HIGHSCORE", 500);
        //StartCoroutine(SpawnEnemy());
        MinPosition = new Vector2(-9f, -4f);
        MaxPosition = new Vector2(9f, 4f);
       // UpdateUI();
    }

    public void UpdateUI()
    {
        textScore.text = string.Format("SCORE\n{0}", score);
        textLife.text = string.Format("LIFE\n{0}", life);
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
        if (life <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    //private IEnumerator SpawnEnemy()
    //{
    //}

}
