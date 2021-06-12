using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    Sprite itemBig, itemSlow, itemLightning;
    SpriteRenderer spriteRenderer = null;

    private GameManager gameManager = null;

    private float speed = 8f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < gameManager.MinPosition.x - 2f)
            Destroy(gameObject);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(spriteRenderer.sprite == itemBig)
            {
                gameManager.playerMove.Item("BigItem");
            }

            else if (spriteRenderer.sprite == itemSlow)
            {
                gameManager.playerMove.Item("SlowItem");
            }
        }
    }
}