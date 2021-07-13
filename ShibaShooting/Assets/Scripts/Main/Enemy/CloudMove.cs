using UnityEngine;

public class CloudMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

    private Rigidbody2D rigid;
    private Collider2D col;
    private SpriteRenderer spriteRenderer;

    protected bool isEnd = false;

    protected virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        col.enabled = false;
    }
    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (!isEnd)
        {
            if (GameManager.Instance.uiManager.ReturnScore() > 2700 && transform.position.x < RandomNumber())
            {
                rigid.gravityScale = 1f;
                col.enabled = true;
                isEnd = true;
            }
        }

        if (transform.position.x < GameManager.Instance.MinPosition.x - 4f || transform.position.y < GameManager.Instance.MinPosition.y - 3f)
        {
            transform.position = new Vector2(12f, 2.7f);
            rigid.gravityScale = 0f;
            col.enabled = false;
            isEnd = false;
            rigid.velocity = Vector2.zero;
        }

        if(GameManager.Instance.uiManager.EnemyHP()==160)
        {
            spriteRenderer.color = new Color(0, 0, 0, 1);
        }
    }

    protected virtual float RandomNumber()
    {
        float random = 0;
        random = Random.Range(-9f, -3f);
        return random;
    }
}
