using System.Collections;
using UnityEngine;

public class SmallEnemyMove : MonoBehaviour
{
    private int hp = 3;
    private int randomRot;

    private Rigidbody2D rigid;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        float randomX, randomY;

        randomX = Random.Range(-1.0f, -0.4f);
        randomY = Random.Range(-1.0f, 1f);

        Vector2 vector2 = new Vector2(randomX, randomY);
        vector2 = vector2.normalized;
        rigid.AddForce(vector2 * 550);
        randomRot = Random.Range(-35, 35);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, randomRot));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(GameManager.Instance.playerMove.ReturnIsBig())
            {
                StartCoroutine(Dead());
                return;
            }

            StartCoroutine(Dead());
            GameManager.Instance.playerMove.StartCoroutine("Damage");
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp--;
            StartCoroutine(Damage());

            if (hp<=0)
            {
                StartCoroutine(Dead());
            }
        }
    }

    IEnumerator Dead()
    {
        col.enabled = false;
        animator.Play("SmallEnemy_Pop");
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
        yield break;
    }

    IEnumerator Damage()
    {
        spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        yield break;
    }
}
