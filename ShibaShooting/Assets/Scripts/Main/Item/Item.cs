using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    Sprite itemBig, itemSlow, itemCoin, itemLightning, itemHeart, itemSmall, itemTired;
    SpriteRenderer spriteRenderer = null;

    private float speed = 8f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < GameManager.Instance.MinPosition.x - 2f)
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
        if (collision.CompareTag(ConstantManager.PLAYER_TAG))
        {
            if (spriteRenderer.sprite == itemCoin)
            {
                SoundManager.Instance.ItemAudio();
                GameManager.Instance.UIManager.AddCoin(5);
                Destroy(gameObject);
            }

            else if (spriteRenderer.sprite == itemHeart)
            {
                SoundManager.Instance.ItemAudio();
                GameManager.Instance.ItemHeart();
                Destroy(gameObject);
            }

            else if (spriteRenderer.sprite == itemTired)
            {
                if (GameManager.Instance.playerMove.ReturnIsTired()) return;
                GameManager.Instance.playerMove.StartCoroutine("ItemTired");
                GameManager.Instance.playerMove.backMove.StartCoroutine("ChangeBackground");
            }

            if (GameManager.Instance.playerMove.ReturnIsItem()) return;


            if (spriteRenderer.sprite == itemBig)
                GameManager.Instance.playerMove.Item("BigItem");

            else if (spriteRenderer.sprite == itemSmall)
                GameManager.Instance.playerMove.Item("SmallItem");

            else if (spriteRenderer.sprite == itemSlow)
                GameManager.Instance.playerMove.StartCoroutine("ItemSlow");
            
            else if (spriteRenderer.sprite == itemLightning)
            {
                SoundManager.Instance.LightningAudio();
                GameManager.Instance.StartCoroutine("SpawnLightning");
            }

            Destroy(gameObject);
        }

    }
}