using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    GameManager gameManager = null;

    [SerializeField]
    private float speed = 3f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if(transform.position.x<gameManager.MinPosition.x-4f)
        {
            Destroy(gameObject);
        }
    }
}
