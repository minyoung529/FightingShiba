using UnityEngine;

public class CoinMove : BulletMove
{
    [SerializeField]
    private float coinSpeed = 4f;

    protected override void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * coinSpeed);
        PoolObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(ConstantManager.PLAYER_TAG))
        {
            SoundManager.Instance.CoinAudio();
            GameManager.Instance.UIManager.AddCoin(1);
            PoolObject();
        }
    }

    private void PoolObject()
    {
        if (transform.position.x < GameManager.Instance.MinPosition.x - 2f)
        {
            transform.SetParent(GameManager.Instance.poolManager.transform);
        }
    }
}
