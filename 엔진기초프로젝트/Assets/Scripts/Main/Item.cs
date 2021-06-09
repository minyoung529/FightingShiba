using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    Sprite itemBig, itemSlow;
    SpriteRenderer spriteRenderer = null;

    private PlayerMove player = null;

    private float speed = 8f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMove>();
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        //if(transform.position)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(spriteRenderer.sprite == itemBig)
            {
                player.Item("BigItem");
                Debug.Log("크게");
            }

            else if (spriteRenderer.sprite == itemSlow)
            {
                player.Item("SlowItem");
                Debug.Log("느리게");
            }
        }
    }
}