using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    
    private float damage = 1f;

    private GameManager gameManager = null;
    private UIManager uiManager = null;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.SetParent(null);
        CheckLimit();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            uiManager.EnemyHPBar(damage);
        }
    }

}
