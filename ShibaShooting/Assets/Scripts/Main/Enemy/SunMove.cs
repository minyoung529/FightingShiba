using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMove : CloudMove
{
    private Collider2D sunCol = null;
    private float sunSpeed = 2.1f;

    [SerializeField]
    SpriteRenderer sunshine;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Sunshine());

        sunCol = GetComponent<Collider2D>();

        sunCol.enabled = false;
    }

    protected override void Update()
    {
        transform.Translate(Vector2.left * sunSpeed * Time.deltaTime);

        if (transform.position.x < gameManager.MinPosition.x - 4f || transform.position.y < gameManager.MinPosition.y - 3f)
        {
            transform.position = new Vector2(12f, 2.8f);
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
