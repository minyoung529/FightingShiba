using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("��� ������Ʈ")]
    [SerializeField]
    private GameObject cloud1 = null;
    [SerializeField]
    private GameObject cloud2 = null;
    [SerializeField]
    private GameObject yellowButterfly = null;

    [Header("������")]
    [SerializeField]
    private GameObject itemPrefab = null;
    [SerializeField]
    private GameObject coinPrefab = null;

    

    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }


    public int life { get; private set; } = 100;

    public PoolManager poolManager { get; private set; }
    public UIManager uiManager { get; private set; }
    public PlayerMove playerMove { get; private set; }


    void Start()
    {
        Time.timeScale = 1;
        poolManager = FindObjectOfType<PoolManager>();
        uiManager = FindObjectOfType<UIManager>();
        playerMove = FindObjectOfType<PlayerMove>();
        MinPosition = new Vector2(-9f, -4f);
        MaxPosition = new Vector2(9f, 4f);

        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnCoin());
    }

    public void Dead()
    {
        life--;
        uiManager.DestroyHeart();
        if (life <= 0)
        {
            if (uiManager.EnemyHP() >= 100)
                SceneManager.LoadScene("GameClear");

            else
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

    private IEnumerator SpawnEnemy()
    {
        float randomY = 0f;
        float randomDelay = 0f;
        int randomNum = 0;

        while (true)
        {
            randomY = Random.Range(-3.5f, 3.5f);
            randomDelay = Random.Range(10f, 15f);
            randomNum = Random.Range(1, 3);

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < 1; i++)
            {
                uiManager.RandomItem(randomNum);
                Instantiate(itemPrefab, new Vector2(12f, randomY), Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private IEnumerator SpawnCoin()
    {
        float randomY = 0f;
        float randomDelay = 0f;

        while (true)
        {
            randomY = Random.Range(-3.5f, 3.5f);
            randomDelay = Random.Range(5f, 10f);

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < 1; i++)
            {
                Instantiate(coinPrefab, new Vector2(12f, randomY), Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }
        }
    }
}