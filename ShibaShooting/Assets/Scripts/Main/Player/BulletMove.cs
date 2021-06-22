using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    protected float speed = 10f;
    protected float damage = 1f;

    protected GameManager gameManager = null;

    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    protected virtual void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.SetParent(null);
        CheckLimit();
    }

    protected virtual void CheckLimit()
    {
        if (transform.position.y > gameManager.MaxPosition.y + 2f)
        {
            Despawn();
        }

        if (transform.position.y < gameManager.MinPosition.y - 2f)
        {
            Despawn();
        }

        if (transform.position.x > gameManager.MaxPosition.x + 2f)
        {
            Despawn();
        }

        if (transform.position.x < gameManager.MinPosition.x - 2f)
        {
            Despawn();
        }
    }

    protected virtual void Despawn()
    {
        transform.SetParent(gameManager.poolManager.transform, false);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            gameManager.uiManager.EnemyHPBar(damage);
            gameManager.uiManager.AddScore(5);
        }
    }
}
