using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    GameManager gameManager = null;

    [SerializeField]
    private float speed = 3f;

    private Rigidbody2D rigid;
    private Collider2D col;

    private bool isEnd = false;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        col.enabled = false;
    }
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (!isEnd)
        {
            if (gameManager.uiManager.ReturnScore() > 600 && transform.position.x < RandomNumber())
            {
                Debug.Log(RandomNumber());
                rigid.gravityScale = 1f;
                col.enabled = true;
                isEnd = true;
            }
        }

        if (transform.position.x < gameManager.MinPosition.x - 4f || transform.position.y < gameManager.MinPosition.y - 3f)
        {
            transform.position = new Vector2(12f, 2.7f);
            rigid.gravityScale = 0f;
            col.enabled = false;
            isEnd = false;
            rigid.velocity = Vector2.zero;
        }
    }

    private float RandomNumber()
    {
        float random = 0;
        random = Random.Range(-9f, -3f);
        return random;
    }
}
