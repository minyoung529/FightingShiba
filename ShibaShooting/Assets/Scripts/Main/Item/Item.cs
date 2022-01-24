using UnityEngine;

public class Item : MonoBehaviour
{
    SpriteRenderer spriteRenderer = null;
    ItemBase item;

    private float speed = 8f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < GameManager.Instance.MinPosition.x - 2f)
            Destroy(gameObject);

        //if (spriteRenderer.sprite == itemLightning || spriteRenderer.sprite == itemTired)
        //{
        //    gameObject.transform.localScale = new Vector2(2f, 2f);
        //    speed = 13f;
        //}

        //else
        //{
        gameObject.transform.localScale = new Vector2(1f, 1f);
        speed = 8f;
        //}
    }

    public void SetItem(ItemBase item)
    {
        this.item = item;
        spriteRenderer.sprite = item.sprite;

    }
    public PlayerState GetItem()
    {
        return item.state;
    }
}