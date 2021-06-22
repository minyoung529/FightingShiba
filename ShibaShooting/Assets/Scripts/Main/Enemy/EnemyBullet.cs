using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletMove
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.Self);
        CheckLimit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Despawn();
        }
    }

    protected override void CheckLimit()
    {
        base.CheckLimit();
    }

    protected override void Despawn()
    {
        transform.SetParent(gameManager.enemyPoolManager.transform, false);
        gameObject.SetActive(false);
    }
}

