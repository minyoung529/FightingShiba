using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : BulletMove
{
    [SerializeField]
    private float coinSpeed = 4f;

    protected override void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * coinSpeed);
        if (transform.position.x < GameManager.Instance.MinPosition.x - 2f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.soundManager.CoinAudio();
            GameManager.Instance.uiManager.AddCoin(1);
            Destroy(gameObject);
        }
    }
}
