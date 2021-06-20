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
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //col = GetComponent<Collider2D>();
        //StartCoroutine(RandomSpawn());
    }

    //private IEnumerator RandomSpawn()
    //{
    //    float randomX;

    //    while(true)
    //    {
    //        gameObject.SetActive(false);
    //        col.enabled = false;
    //        yield return new WaitForSeconds(2f);

    //        randomX = Random.Range(-8f, -3f);
    //        transform.position = new Vector2(randomX, 0f);
    //        gameObject.SetActive(true);
    //        Debug.Log("¾Æ");

    //        spriteRenderer.sprite = defaultSprite;
    //        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
    //        yield return new WaitForSeconds(1f);

    //        col.enabled = true;
    //        spriteRenderer.sprite = lightning;
    //        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    //    }
    //}
}
