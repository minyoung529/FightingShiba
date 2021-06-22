using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMove : CloudMove
{
    private Collider2D col = null;
    private Rigidbody2D rigid;

    [SerializeField]
    SpriteRenderer sunshine;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Sunshine());

        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        col.enabled = false;
    }

    protected override void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < gameManager.MinPosition.x - 4f || transform.position.y < gameManager.MinPosition.y - 3f)
        {
            transform.position = new Vector2(12f, 2.8f);
        }

        if (gameManager.playerMove.GetIsTired())
        {
            sunshine.color = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            sunshine.color = new Color(1f, 1f, 1f, 0.9f);
        }
    }

    private IEnumerator Sunshine()
    {
        yield return new WaitForSeconds(10f);

        while (true)
        {
            
            sunshine.enabled = true;
            yield return new WaitForSeconds(2f);
            sunshine.enabled = false;
            yield return new WaitForSeconds(6f);

            yield return new WaitForSeconds(15f);
        }
    }
}
