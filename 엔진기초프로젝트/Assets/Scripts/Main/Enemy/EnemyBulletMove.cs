using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    private GameManager gameManager = null;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.Self);

        CheckLimit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Despawn();
    }

    private void CheckLimit()
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

    private void Despawn()
    {
        transform.SetParent(gameManager.poolManager.transform, false);
        gameObject.SetActive(false);
    }
}

