using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private int score = 100;
    [SerializeField]
    private int hp = 50;
    [SerializeField]
    protected float speed = 5f;
    private bool isDamaged = false;
    protected bool isDead = false;
    protected GameManager gameManager = null;
    private Animator animator = null;
    private SpriteRenderer spriteRenderer = null;

    private float timer = 0f;
    private Vector3 diff = Vector3.zero;
    private PlayerMove player = null;
    private float rotationZ = 0f;

    [SerializeField]
    private float fireRate = 0.8f;

    [SerializeField]
    private Transform enemyBulletPosition = null;
    [SerializeField]
    private GameObject enemyBulletPrefab = null;

    private GameObject bullet = null;

    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        player = FindObjectOfType<PlayerMove>();
    }

    protected virtual void Update()
    {
        if (isDead) return;

        if(transform.position.x>=7f)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        Fire();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return; //ÇÑ¹ø¸¸ Á×ÀÓ, Á×ÀÌ¸é ÃÑ¾Ë Åë°ú

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
        spriteRenderer.material.SetColor("_Color", new Color(1f, 0f, 0f, 0f));
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
            bullet = Instantiate(enemyBulletPrefab, transform);
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ-180f);
            bullet.transform.SetParent(null);
        }

        if (transform.position.x < gameManager.MinPosition.x - 2f)
        {
            Destroy(gameObject);
        }
    }

}
