using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("πË∞Ê ø¿∫Í¡ß∆Æ")]
    [SerializeField]
    private GameObject cloud1 = null;
    [SerializeField]
    private GameObject cloud2 = null;
    [SerializeField]
    private GameObject yellowButterfly = null;

    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }

   
    public int life { get; private set; } = 3;

    public PoolManager poolManager{ get; private set; }
    public UIManager uiManager { get; private set; }

    void Start()
    {
        Debug.Log("≥»≥»");
        Time.timeScale = 1;

        poolManager = FindObjectOfType<PoolManager>();
        uiManager = FindObjectOfType<UIManager>();
        MinPosition = new Vector2(-9f, -4f);
        MaxPosition = new Vector2(9f, 4f);
       // UpdateUI();
    }

    public void Dead()
    {
        life--;
        uiManager.DestroyHeart();
        if (life <= 0)
        {
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

    public void AddScore(int score)
    {
        uiManager.AddScore(score);
    }
}
