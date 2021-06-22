using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("아이템")]
    [SerializeField]
    private GameObject itemPrefab = null;
    [SerializeField]
    private GameObject coinPrefab = null;

    [Header("번개")]
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

    [Header("배경")]
    [SerializeField]
    private GameObject cloud;
    [SerializeField]
    private GameObject cloud2;

    public Vector2 MinPosition { get; private set; }
    public Vector2 MaxPosition { get; private set; }

    public int Life { get; private set; } = 3;

    public PoolManager poolManager { get; private set; }
    public EnemyPoolManager enemyPoolManager { get; private set; }
    public UIManager uiManager { get; private set; }
    public PlayerMove playerMove { get; private set; }
    public SoundManager soundManager { get; private set; }


    void Start()
    {
        Time.timeScale = 1;
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

    public void Dead()
    {
        Life--;
        uiManager.DestroyHeart();
        if (Life <= 0)
        {
            if (uiManager.EnemyHP() > 150)
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

    private IEnumerator SpawnItem()
    {
        float randomY = 0f;
        float randomDelay = 0f;
        int ramdomNum = 0;

        while (true)
        {
            randomY = Random.Range(-3.5f, 3.5f);
            randomDelay = Random.Range(6f, 7f);
            ramdomNum = Random.Range(0, 8);

            yield return new WaitForSeconds(randomDelay);

            uiManager.RandomItem(ramdomNum);
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
    }

    private IEnumerator SpawnLightning()
    {
        if (playerMove.GetIsItem()) yield break;
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
        yield break;
    }

    public void ItemHeart()
    {
        if (Life == 3) return;

        if(Life < 3)
        {
            Life++;
            uiManager.ActiveHeart();
        }
    }
}