using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Skins : MonoBehaviour
{
    [SerializeField]
    private GameObject contents;
    [SerializeField]
    private GameObject check;
    private string crtShiba;

    Image image;

    ShopSoundManager shopSoundManager;

    void Start()
    {
        shopSoundManager = FindObjectOfType<ShopSoundManager>();
        crtShiba = PlayerPrefs.GetString("Shiba", "isIdle");
        image = GetComponent<Image>();
        image.color = new Color(0.5f, 0.5f, 0.5f, 1f);
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        if (gameObject.transform == contents.transform.GetChild(0))
        {
            image.color = new Color(1f, 1f, 1f, 1f);
            crtShiba = "isIdle";
        }

        if (gameObject.transform == contents.transform.GetChild(1) && PlayerPrefs.GetString("isBuyStrawberry") == "true")
        {
            image.color = new Color(1f, 1f, 1f, 1f);
            crtShiba = "isStrawberry";
        }

        if (gameObject.transform == contents.transform.GetChild(2) && PlayerPrefs.GetString("isMint") == "true")
        {
            image.color = new Color(1f, 1f, 1f, 1f);
            crtShiba = "isMint";
        }

        if (gameObject.transform == contents.transform.GetChild(3) && PlayerPrefs.GetString("isDevil") == "true")
        {
            image.color = new Color(1f, 1f, 1f, 1f);
            crtShiba = "isDevil";
        }

        if (gameObject.transform == contents.transform.GetChild(4) && PlayerPrefs.GetString("isAngel") == "true")
        {
            image.color = new Color(1f, 1f, 1f, 1f);
            crtShiba = "isAngel";
        }

        if (gameObject.transform == contents.transform.GetChild(5) && PlayerPrefs.GetString("isMelona") == "true")
        {
            image.color = new Color(1f, 1f, 1f, 1f);
            crtShiba = "isMelona";
        }
    }

    public void OnClickChoose()
    {
        shopSoundManager.ButtonSound();

        if(image.color == new Color(1f, 1f, 1f, 1f))
        {
            check.transform.localScale = Vector2.zero;
            check.transform.DOScale(Vector2.one, 0.2f);
            check.transform.position = new Vector2(transform.position.x, transform.position.y);
            PlayerPrefs.SetString("Shiba", crtShiba);
        }
    }
}
