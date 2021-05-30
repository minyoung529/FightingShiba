using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    #region 변수목록
    [Header("처치시 주는 점수")]
    [SerializeField]
    private int score = 100;

    [Header("Enemy 체력")]
    [SerializeField]
    private float hp = 100f;

    [Header("Enemy 빠르기")]
    [SerializeField]
    protected float speed = 5f;

    private bool isDamaged = false;
    protected bool isDead = false;

    protected GameManager gameManager = null;
    private Animator animator = null;
    private SpriteRenderer spriteRenderer = null;

    private SpeechBubble speechBubble = null;

    [Header("총알 딜레이")]
    [SerializeField]
    private float fireRate = 0.8f;
    private GameObject bullet = null;

    private float timer = 0f;
    private float circleTimer = 0f;
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
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        player = FindObjectOfType<PlayerMove>();
        speechBubble = FindObjectOfType<SpeechBubble>();
    }

    protected virtual void Update()
    {
        if (isDead) return;

        if (transform.position.x >= 6.5f)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        EnemyAttack();
        Fire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 6.5f) return;
        if (isDead) return;

        if (collision.CompareTag("Bullet"))
        {
            if (isDamaged) return;
            isDamaged = true;
            Destroy(collision.gameObject);
            StartCoroutine(Damaged());

            if (hp <= 0)
            {
                isDead = true;
                gameManager.AddScore(score);
                StartCoroutine(Dead());
            }
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

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void Fire()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            diff = player.transform.position - transform.position;
            diff.Normalize();

            rotationZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            timer = 0f;
            bullet = Instantiate(enemyBulletPrefab, enemyBulletPosition.transform);
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 180f);
            bullet.transform.SetParent(null);
        }

        if (transform.position.x < gameManager.MinPosition.x - 2f)
        {
            Destroy(gameObject);
        }
    }

    private void EnemyAttack()
    {
        if (hp == 90)
        {
            fireRate = 0.65f;
            spriteRenderer.sprite = firstMinyoung;
        }

        if (hp == 75)
        {
            fireRate = 0.55f;
            spriteRenderer.sprite = secondMinyoung;
            speechBubble.ChangeToUnityTrow();
        }

        if (hp == 50)
        {
            spriteRenderer.sprite = thirdMinyoung;
        }

        if (hp < 50)
        {
            Circle();
        }

        if (hp == 30)
        {
            spriteRenderer.sprite = fourthMinyoung;
        }
    }

    private void Circle()
    {

        circleTimer += Time.deltaTime;

        if (circleTimer >= 3f)
        {
            for (int i = -90; i < 90; i += 13)
            {
                    GameObject circleBullet = Instantiate(enemyBulletPrefab);

                    circleBullet.transform.position = enemyBulletPosition.transform.position;
                    circleBullet.transform.rotation = Quaternion.Euler(0, 0, i);
            }
        }
    }
}
