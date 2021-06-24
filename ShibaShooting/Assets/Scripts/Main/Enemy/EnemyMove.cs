using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    #region 변수목록
    [Header("Enemy 체력")]
    [SerializeField]
    private float hp = 100f;

    [Header("Enemy 빠르기")]
    [SerializeField]
    protected float speed = 5f;

    private bool isDamaged = false;
    protected bool isDead = false;

    protected GameManager gameManager = null;
    private SpriteRenderer spriteRenderer = null;
    private SpeechBubble speechBubble = null;

    [Header("총알 딜레이")]
    [SerializeField]
    private float fireRate = 0.8f;
    private GameObject bullet = null;

    private float timer = 0f;
    private float circleTimer = 0f;
    private float circleMaxTime = 3f;
    private Vector3 diff = Vector3.zero;
    private PlayerMove player = null;
    private float rotationZ = 0f;

    [Header("적 생성시 필요한 변수")]
    [SerializeField]
    private Transform enemyBulletPosition = null;
    [SerializeField]
    private GameObject enemyBulletPrefab = null;

    [Header("스프라이트")]
    [SerializeField]
    private Sprite firstMinyoung;
    [SerializeField]
    private Sprite secondMinyoung;
    [SerializeField]
    private Sprite thirdMinyoung;
    [SerializeField]
    private Sprite fourthMinyoung;
    #endregion

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerMove>();
        speechBubble = FindObjectOfType<SpeechBubble>();

        StartCoroutine(EnemyFire());
    }

    protected virtual void Update()
    {
        if (isDead) return;

        if (transform.position.x >= 6.5f)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        EnemyAttack();
        ChangeSprite();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 6.5f) return;
        if (isDead) return;

        if (collision.CompareTag("Bullet"))
        {
            if (isDamaged) return;
            isDamaged = true;
            collision.gameObject.SetActive(false);
            collision.transform.SetParent(gameManager.poolManager.transform, false);
            StartCoroutine(Damaged());
        }
    }
    private IEnumerator Damaged()
    {
        hp--;
        spriteRenderer.material.SetColor("_Color", new Color(0.5f, 0.5f, 0.5f, 0f));
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
        isDamaged = false;
    }

    private IEnumerator EnemyFire()
    {
        while (true)
        {
            InstantiateOrPool();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private GameObject Fire(GameObject bullet)
    {
        diff = player.transform.position - transform.position;
        diff.Normalize();

        rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        timer = 0f;

        bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 180f);
        bullet.transform.SetParent(null);

        return bullet;
    }

    protected virtual GameObject InstantiateOrPool()
    {
        GameObject result = null;

        if (gameManager.enemyPoolManager.transform.childCount > 0)
        {
            result = gameManager.enemyPoolManager.transform.GetChild(0).gameObject;
            result.transform.position = enemyBulletPosition.position;
            result.transform.SetParent(null);
            result.SetActive(true);
        }

        else
        {
            result = Instantiate(enemyBulletPrefab, enemyBulletPosition.transform);
        }

        result = Fire(result);
        return result;
    }

    private void EnemyAttack()
    {
        if (hp < 90)
        {
            fireRate = 0.65f;
            speechBubble.ChangeSprites(1);
        }

        if (hp < 80)
            speechBubble.ChangeSprites(2);

        if (hp < 70)
            speechBubble.ChangeSprites(3);
            fireRate = 0.5f;

        if (hp < 60)
        {
            speechBubble.ChangeSprites(4);
        }

        if (hp < 50)
        {
            Circle();
            speechBubble.ChangeSprites(5);
        }

        if (hp < 40)
        {
            circleMaxTime = 2f;
            speechBubble.ChangeSprites(6);
        }

        if (hp < 30)
        {
            circleMaxTime = 1.5f;
            speechBubble.ChangeSprites(7);
        }

        if (hp < 20)
        {
            circleMaxTime = 1.2f;
            speechBubble.ChangeSprites(8);
        }

        else if (hp < 0)
            circleMaxTime = 0.9f;
    }

    private void ChangeSprite()
    {
        if (hp < 80)
            spriteRenderer.sprite = firstMinyoung;

        if (hp < 60)
            spriteRenderer.sprite = secondMinyoung;

        if (hp < 40)
            spriteRenderer.sprite = thirdMinyoung;

        if (hp < 20)
            spriteRenderer.sprite = fourthMinyoung;
    }

    private void Circle()
    {
        circleTimer += Time.deltaTime;

        GameObject circleBullet = null;

        if (circleTimer >= 3f)
        {
            for (int i = -90; i < 90; i += 15)
            {
                if (gameManager.enemyPoolManager.transform.childCount > 0)
                {
                    circleBullet = gameManager.enemyPoolManager.transform.GetChild(0).gameObject;
                    circleBullet.transform.position = enemyBulletPosition.position;
                    circleBullet.transform.SetParent(null);
                    circleBullet.SetActive(true);
                }

                else
                    circleBullet = Instantiate(enemyBulletPrefab, enemyBulletPosition.transform);

                circleBullet.transform.position = enemyBulletPosition.transform.position;
                circleBullet.transform.rotation = Quaternion.Euler(0, 0, i);
                circleTimer = 0f;
            }
        }
    }
}