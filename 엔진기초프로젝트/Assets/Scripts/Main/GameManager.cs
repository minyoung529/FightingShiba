using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("배경 오브젝트")]
    [SerializeField]
    private GameObject cloud1 = null;
    [SerializeField]
    private GameObject cloud2 = null;
    [SerializeField]
    private GameObject yellowButterfly = null;

    [Header("아이템")]
    [SerializeField]
    private GameObject itemPrefab = null;

    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }


    public int life { get; private set; } = 3;

    public PoolManager poolManager { get; private set; }
    public UIManager uiManager { get; private set; }
    public Item item;

    void Start()
    {
        Time.timeScale = 1;
        poolManager = FindObjectOfType<PoolManager>();
        uiManager = FindObjectOfType<UIManager>();
        MinPosition = new Vector2(-9f, -4f);
        MaxPosition = new Vector2(9f, 4f);

        StartCoroutine(SpawnEnemy());
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
}
