using UnityEngine;

public class CoinMove : BulletMove
{
    [SerializeField] private float coinSpeed = 4f;

    protected override void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * coinSpeed);
        SetPoolObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(ConstantManager.PLAYER_TAG))
        {
            SoundManager.Instance.CoinAudio();
            GameManager.Instance.UIManager.AddCoin(1);
            GameManager.Instance.poolManager.DespawnPoolObject(gameObject);
        }
    }

    private void SetPoolObject()
    {
        if (transform.position.x < GameManager.Instance.MinPosition.x - 2f)
        {
            GameManager.Instance.poolManager.DespawnPoolObject(gameObject);
        }
    }
}
