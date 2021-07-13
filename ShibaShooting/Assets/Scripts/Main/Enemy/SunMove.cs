using System.Collections;
using UnityEngine;

public class SunMove : CloudMove
{
    private float sunSpeed = 2.1f;

    [SerializeField]
    SpriteRenderer sunshine;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Sunshine());
    }

    private void Update()
    {
        transform.Translate(Vector2.left * sunSpeed * Time.deltaTime);

        if (transform.position.x < GameManager.Instance.MinPosition.x - 4f || transform.position.y < GameManager.Instance.MinPosition.y - 3f)
        {
            transform.position = new Vector2(12f, 2.8f);
        }
    }

    private IEnumerator Sunshine()
    {
        yield return new WaitForSeconds(60f);

        while (true)
        {
            sunshine.enabled = true;
            yield return new WaitForSeconds(2f);
            sunshine.enabled = false;
            yield return new WaitForSeconds(18f);
        }
    }
}
