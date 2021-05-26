using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 4f;

    GameManager gameManager = null;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < gameManager.MinPosition.x - 2f)
        {
            Destroy(gameObject);
        }
    }
}
