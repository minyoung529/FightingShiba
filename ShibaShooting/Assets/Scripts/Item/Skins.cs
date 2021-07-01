using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skins : MonoBehaviour
{
    [SerializeField]
    private GameObject contents;
    [SerializeField]
    private GameObject check;
    private string crtShiba;

    Image image;

    void Start()
    {
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
    }

    public void OnClickChoose()
    {
        if(image.color == new Color(1f, 1f, 1f, 1f))
        {
            check.SetActive(true);
            check.transform.position = new Vector2(transform.position.x, transform.position.y);
            PlayerPrefs.SetString("Shiba", crtShiba);
        }
    }
}
