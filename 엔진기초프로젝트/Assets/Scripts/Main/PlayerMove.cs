using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("�÷��̾� ���ǵ�")]
    [SerializeField]
    private float speed = 5f;
    [Header("�Ѿ� ������")]
    [SerializeField]
    private Transform bulletPosition = null;
    [Header("�Ѿ� ������")]
    [SerializeField]
    private GameObject bulletPrefab = null;
    [Header("�Ѿ� ������ �ð�")]
    [SerializeField]
    private float fireRate = 0.5f;
    [Header("����")]
    [SerializeField]
    private AudioSource music;

    private Vector2 targetPosition = Vector2.zero;
    private GameManager gameManager = null;
    private SpriteRenderer spriteRenderer = null;
    private Rigidbody2D rigid = null;

    private bool isDamaged = false;
    public bool IsBig { get; private set; } = false;

    private float countTime;
    private float bigCooltime = 3f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
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
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            InstantiateOrPool();
            yield return new WaitForSeconds(fireRate);
        }
    }

    //instantiating or pooling player bullet
    private GameObject InstantiateOrPool()
    {
        GameObject result = null;

        if (gameManager.poolManager.transform.childCount > 0)
        {
            result = gameManager.poolManager.transform.GetChild(0).gameObject;
            result.transform.position = bulletPosition.position;
            result.transform.SetParent(null);
            result.SetActive(true);

            gameManager.uiManager.AddScore(2);
        }

        else
        {
            GameObject newBullet = Instantiate(bulletPrefab, bulletPosition);
            newBullet.transform.position = bulletPosition.position;
            newBullet.transform.SetParent(null);
            result = newBullet;

            gameManager.uiManager.AddScore(2);
        }
        return result;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("Coin"))
        {
            gameManager.uiManager.AddCoin(1);
            gameManager.soundManager.CoinAudio();
        }

        if (isDamaged) return;

        if (collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            if (IsBig) return;
            gameManager.soundManager.DamagedAudio();
            StartCoroutine(Damage());
        }

        if (collision.CompareTag("Lightning"))
        {
            StartCoroutine(Damage());
        }
    }

    // Damage effect
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
        if (item == "BigItem")
        {
            gameManager.soundManager.ItemAudio();
            StartCoroutine(ItemBig(1.5f));
        }

        else if (item == "SlowItem")
        {
            gameManager.soundManager.ItemAudio();
            StartCoroutine(ItemSlow());
        }

        else if (item == "LightningItem")
        {
            gameManager.soundManager.LightningAudio();
            gameManager.StartCoroutine("SpawnLightning");
        }

        else if (item == "HeartItem")
        {
            gameManager.soundManager.ItemAudio();
            gameManager.ItemHeart();
        }
        else if (item == "SmallItem")
        {
            gameManager.soundManager.ItemAudio();
            StartCoroutine(ItemBig(0.7f));
        }
        countTime = 0f;
    }

    public IEnumerator ItemBig(float scale)
    {
        IsBig = true;

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
        countTime = 6f;
        IsBig = false;
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

        countTime = 6f;
        Time.timeScale = 1f;
        gameManager.soundManager.DefaultSpeed();
        yield break;
    }
}
