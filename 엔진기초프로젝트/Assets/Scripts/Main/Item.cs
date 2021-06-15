using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    Sprite itemBig, itemSlow, itemCoin, itemLightning;
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
        if (spriteRenderer.sprite == itemLightning)
        {
            gameObject.transform.localScale = new Vector2(2f, 2f);
            speed = 13f;
        }
        else
        {
            gameObject.transform.localScale = new Vector2(1f, 1f);
            speed = 8f;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (spriteRenderer.sprite == itemBig)
            {
                gameManager.playerMove.Item("BigItem");
            }

            else if (spriteRenderer.sprite == itemSlow)
            {
                gameManager.playerMove.Item("SlowItem");
            }

            else if (spriteRenderer.sprite == itemCoin)
            {
                gameManager.uiManager.AddCoin(5);
            }

            else if (spriteRenderer.sprite == itemLightning)
            {
                gameManager.playerMove.Item("LightningItem");

            }
        }
    }
}