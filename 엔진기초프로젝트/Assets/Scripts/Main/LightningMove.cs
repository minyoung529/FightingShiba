using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningMove : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    [SerializeField]
    private Sprite lightning;
    [SerializeField]
    private Sprite defaultSprite;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        StartCoroutine(RandomSpawn());
    }

    private IEnumerator RandomSpawn()
    {
        float randomX;

        while(true)
        {
            yield return new WaitForSeconds(6f);

            randomX = Random.Range(-8f, -3f);
            transform.position = new Vector2(randomX, 0f);
            gameObject.SetActive(true);

            //spriteRenderer.sprite = defaultSprite;
            yield return new WaitForSeconds(0.5f);

            col.enabled = true;
            //spriteRenderer.sprite = lightning;
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false);
            col.enabled = false;
        }
    }
}
