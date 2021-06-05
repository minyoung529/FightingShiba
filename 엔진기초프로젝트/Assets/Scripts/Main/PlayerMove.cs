using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("ÇÃ·¹ÀÌ¾î ½ºÇÇµå")]
    [SerializeField]
    private float speed = 5f;
    [Header("ÃÑ¾Ë Æ÷Áö¼Ç")]
    [SerializeField]
    private Transform bulletPosition = null;
    [Header("ÃÑ¾Ë ÇÁ¸®ÆÕ")]
    [SerializeField]
    private GameObject bulletPrefab = null;
    [Header("ÃÑ¾Ë µô·¹ÀÌ ½Ã°£")]
    [SerializeField]
    private float fireRate = 0.5f;

    private Vector2 targetPosition = Vector2.zero;
    private GameManager gameManager = null;
    private SpriteRenderer spriteRenderer = null;

    private bool isDamaged = false;
    private bool isBig = false;
    private bool isItem = false;

    private float countTime;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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

    //instantiating or pooling player bullet
    private GameObject InstantiateOrPool()
    {
        GameObject result = null;

        if (gameManager.poolManager.transform.childCount > 0 && gameManager.poolManager.transform.GetChild(0).CompareTag("Bullet"))
        {
            result = gameManager.poolManager.transform.GetChild(0).gameObject;
            result.transform.position = bulletPosition.position;
            result.transform.SetParent(null);
            result.SetActive(true);

            gameManager.uiManager.AddScore(1);
        }

        else
        {
            GameObject newBullet = Instantiate(bulletPrefab, bulletPosition);
            newBullet.transform.position = bulletPosition.position;
            newBullet.transform.SetParent(null);
            result = newBullet;

            gameManager.uiManager.AddScore(1);
        }
        return result;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDamaged) return;

        if (collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);
            if (isBig) return;
            StartCoroutine(Damage());
        }

        else if(collision.CompareTag("Item"))
        {
            if (isItem) return;
            isItem = true;
            Destroy(collision.gameObject);
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
        if (isItem) return;

        if(item == "BigItem")
        StartCoroutine("ItemBig");

        else if (item == "SlowItem")
        StartCoroutine("ItemSlow");

        countTime = 0f;
    }

    public IEnumerator ItemBig()
    {
        isBig = true;

        gameObject.transform.localScale = new Vector2(1.5f, 1.5f);
        yield return new WaitForSeconds(5f);

        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.material.SetColor("_Color", new Color(0.2f, 0.2f, 0.2f, 0f));
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
            yield return new WaitForSeconds(0.2f);
        }

        gameObject.transform.localScale = new Vector2(0.9f, 0.9f);
        countTime = 6f;
        isBig = false;
        isItem = false;
        StopCoroutine("ItemBig");

    }

    public IEnumerator ItemSlow()
    {
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
        isItem = false;
        StopCoroutine("ItemSlow");
    }
}
