using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;
    private GameManager gameManager = null;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);
        if (transform.position.x < gameManager.MinPosition.x - 2f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
