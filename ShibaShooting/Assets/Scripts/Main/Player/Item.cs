using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    Sprite itemBig, itemSlow, itemCoin, itemLightning, itemHeart, itemSmall, itemTired;
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
        if (spriteRenderer.sprite == itemLightning || spriteRenderer.sprite == itemTired)
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
            if (spriteRenderer.sprite == itemCoin)
            {
                gameManager.soundManager.ItemAudio();
                gameManager.uiManager.AddCoin(5);
                Destroy(gameObject);
            }

            else if (spriteRenderer.sprite == itemHeart)
            {
                gameManager.soundManager.ItemAudio();
                gameManager.ItemHeart();
                Destroy(gameObject);
            }

            else if (spriteRenderer.sprite == itemTired)
            {
                if (gameManager.playerMove.ReturnIsTired()) return;
                gameManager.playerMove.StartCoroutine("ItemTired");
                gameManager.playerMove.backMove.StartCoroutine("ChangeBackground");
            }

            if (gameManager.playerMove.ReturnIsItem()) return;


            if (spriteRenderer.sprite == itemBig)
                gameManager.playerMove.Item("BigItem");

            else if (spriteRenderer.sprite == itemSmall)
                gameManager.playerMove.Item("SmallItem");

            else if (spriteRenderer.sprite == itemSlow)
                gameManager.playerMove.StartCoroutine("ItemSlow");
            
            else if (spriteRenderer.sprite == itemLightning)
            {
                gameManager.soundManager.LightningAudio();
                gameManager.StartCoroutine("SpawnLightning");
            }

            Destroy(gameObject);
        }

    }
}