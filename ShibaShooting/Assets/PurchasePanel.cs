using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePanel : MonoBehaviour
{
    [SerializeField] private Image image;
    private Text priceText;

    private void Start()
    {
        priceText = GetComponentInChildren<Text>();
    }

    public void Init(ItemBase item)
    {
        priceText.text = item.price.ToString();
        image.sprite = item.sprite;
    }
}
