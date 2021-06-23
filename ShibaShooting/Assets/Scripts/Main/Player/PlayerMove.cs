using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private float speed = 30f;

    [Header("ÃÑ¾Ë Æ÷Áö¼Ç")]
    [SerializeField]
    private Transform bulletPosition = null;
    [Header("ÃÑ¾Ë ÇÁ¸®ÆÕ")]
    [SerializeField]
    private GameObject bulletPrefab = null;
    [Header("ÃÑ¾Ë µô·¹ÀÌ ½Ã°£")]
    [SerializeField]
    private float fireRate = 0.5f;
    [Header("À½¾Ç")]
    [SerializeField]
    private AudioSource music;
    [Header("²Ù¹Ì±â")]
    [SerializeField]
    private SpriteRenderer deco;
    [SerializeField]
    private Sprite sleep;
    [SerializeField]
    private Sprite lightning;

    private Vector2 targetPosition = Vector2.zero;
    private GameManager gameManager = null;
    private BackgroundMove backMove = null;
    private SpriteRenderer spriteRenderer = null;

    private bool isDamaged = false;
    private bool isBig = false;
    private bool isItem = false;
    private bool isTired = false;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        backMove = FindObjectOfType<BackgroundMove>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Fire());
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            targetPosition.x = Mathf.Clamp(targetPosition.x, gameManager.MinPosition.x, gameManager.MaxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, gameManager.MinPosition.y, gameManager.MaxPosition.y);

            transform.localPosition =
            Vector2.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
        }

        Debug.Log(isItem);
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            InstantiateOrPool();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private GameObject InstantiateOrPool()
    {
        GameObject result = null;

        if (gameManager.poolManager.transform.childCount > 0)
        {
            result = gameManager.poolManager.transform.GetChild(0).gameObject;
            result.transform.position = bulletPosition.position;
            result.transform.SetParent(null);
            result.SetActive(true);

            gameManager.uiManager.AddScore(4);
        }

        else
        {
            GameObject newBullet = Instantiate(bulletPrefab, bulletPosition);
            newBullet.transform.position = bulletPosition.position;
            newBullet.transform.SetParent(null);
            result = newBullet;

            gameManager.uiManager.AddScore(4);
        }
        return result;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            gameManager.uiManager.AddCoin(1);
            gameManager.soundManager.CoinAudio();
        }

        if (isDamaged) return;

        if (collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            if (isBig) return;
            gameManager.soundManager.DamagedAudio();
            StartCoroutine(Damage());
        }

        if (collision.CompareTag("Lightning"))
        {
            gameManager.soundManager.DamagedAudio();
            StartCoroutine(Damage());
        }
    }

    private IEnumerator Damage()
    {
        isDamaged = true;
        gameManager.Dead();
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        isDamaged = false;
    }

    public void Item(string item)
    {
        switch (item)
        {

            case "BigItem":
                isItem = true;
                gameManager.soundManager.ItemAudio();
                StartCoroutine(ItemBig(1.5f));
                break;

            case "SlowItem":
                isItem = true;
                gameManager.soundManager.ItemAudio();
                StartCoroutine(ItemSlow());
                break;

            case "LightningItem":
                isItem = true;
                gameManager.soundManager.LightningAudio();
                gameManager.StartCoroutine("SpawnLightning");
                break;

            case "HeartItem":
                gameManager.soundManager.ItemAudio();
                gameManager.ItemHeart();
                break;

            case "SmallItem":
                isItem = true;
                gameManager.soundManager.ItemAudio();
                StartCoroutine(ItemBig(0.7f));
                break;

            case "TiredItem":
                isItem = true;
                backMove.StartCoroutine("ChangeBackground");
                StartCoroutine(ItemTired());
                break;

            default:
                break;
        }
    }

    public IEnumerator ItemBig(float scale)
    {
        if(scale == 1.5f)
        {
            isBig = true;
        }

        gameObject.transform.localScale = new Vector2(scale, scale);
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.material.SetColor("_Color", new Color(0.2f, 0.2f, 0.2f, 0f));
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
            yield return new WaitForSeconds(0.2f);
        }

        gameObject.transform.localScale = new Vector2(0.9f, 0.9f);
        isBig = false;
        isItem = false;
        yield break;
    }

    public IEnumerator ItemSlow()
    {
        gameManager.soundManager.Slow();
        Time.timeScale = 0.5f;
        yield return new WaitForSecondsRealtime(5f);

        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.material.SetColor("_Color", new Color(0.2f, 0.2f, 0.2f, 0f));
            yield return new WaitForSecondsRealtime(0.2f);
            spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
            yield return new WaitForSecondsRealtime(0.2f);
        }

        Time.timeScale = 1f;
        gameManager.soundManager.DefaultSpeed();
        isItem = false;
        yield break;
    }

    public IEnumerator ItemTired()
    {
        isTired = true;
        deco.enabled = true;
        deco.sprite = sleep;
        speed = 7f;

        yield return new WaitForSecondsRealtime(5f);

        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.material.SetColor("_Color", new Color(0.2f, 0.2f, 0.2f, 0f));
            deco.enabled = false;
            yield return new WaitForSecondsRealtime(0.2f);
            deco.enabled = true;
            spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
            yield return new WaitForSecondsRealtime(0.2f);
        }

        speed = 30f;
        deco.enabled = false;
        isItem = false;
        isTired = false;
        yield break;
    }

    public void DecoLightning(bool isTrue)
    {
        deco.enabled = isTrue;
        deco.sprite = lightning;
    }

    public void IsItem(bool isTrue)
    {
        isItem = isTrue;
    }

    public bool ReturnIsItem()
    {
        return isItem;
    }
}