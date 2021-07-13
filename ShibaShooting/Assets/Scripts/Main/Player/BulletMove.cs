using UnityEngine;

public class BulletMove : MonoBehaviour
{
    protected float speed = 10f;
    protected float damage = 1f;

    protected virtual void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.SetParent(null);
        CheckLimit();
    }

    protected virtual void CheckLimit()
    {
        if (transform.position.y > GameManager.Instance.MaxPosition.y + 2f)
        {
            Despawn();
        }

        if (transform.position.y < GameManager.Instance.MinPosition.y - 2f)
        {
            Despawn();
        }

        if (transform.position.x > GameManager.Instance.MaxPosition.x + 2f)
        {
            Despawn();
        }

        if (transform.position.x < GameManager.Instance.MinPosition.x - 2f)
        {
            Despawn();
        }
    }

    protected virtual void Despawn()
    {
        transform.SetParent(GameManager.Instance.poolManager.transform, false);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameManager.Instance.uiManager.AddScore(5);
        }
    }
}
