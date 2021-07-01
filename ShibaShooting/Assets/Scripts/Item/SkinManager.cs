using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    private string crtShiba;
    private string soldShiba;

    [SerializeField]
    private GameObject buyPopup, contents;
    [SerializeField]
    private Text coinText, playerCoin;
    private int productCoin, playerMoney;

    void Start()
    {
        crtShiba = PlayerPrefs.GetString("Shiba", "isIdle");
        playerMoney = PlayerPrefs.GetInt("COIN", 0);
    }

    public void OnClickIdle()
    {
        crtShiba = "isIdle";
        PlayerPrefs.SetString("Shiba", crtShiba);
    }

    public void OnClickNo()
    {
        buyPopup.SetActive(false);
    }

    private void UpdateUI()
    {
        playerMoney = PlayerPrefs.GetInt("COIN", 0);
        playerCoin.text = string.Format("{0}", playerMoney);
    }
    public void OnClickYes()
    {
        playerMoney = PlayerPrefs.GetInt("COIN", 0);

        if (playerMoney - productCoin <= 0)
        {
            Debug.Log("µ·¾÷½á");
            return;
        }

        switch (soldShiba)
        {
            case "Strawberry":
                PlayerPrefs.SetString("isBuyStrawberry", "true");
                playerMoney -= productCoin;
                break;

            case "Mint":
                PlayerPrefs.SetString("isMint", "true");
                playerMoney -= productCoin;
                break;
        }

        PlayerPrefs.SetInt("COIN", playerMoney);
        Debug.Log(playerMoney);
        UpdateUI();
        ChangeSprite();
        buyPopup.SetActive(false);
    }

    public void OnClickStrawberry()
    {
        if(PlayerPrefs.GetString("isBuyStrawberry") != "true")
        {
            productCoin = 100;
            buyPopup.SetActive(true);
            coinText.text = string.Format("{0}¿ø", productCoin);
            soldShiba = "Strawberry";
        }

        if (PlayerPrefs.GetString("isMint") != "true")
        {
            productCoin = 150;
            buyPopup.SetActive(true);
            coinText.text = string.Format("{0}¿ø", productCoin);
            soldShiba = "Mint";
        }
    }

    private void ChangeSprite()
    {
        for(int i = 0; i< contents.transform.childCount; i++)
        {
            contents.transform.GetChild(i).GetComponent<Skins>().ChangeSprite();
        }
    }
}
