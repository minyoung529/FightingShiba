using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{
    private string crtShiba;
    private string soldShiba;

    [SerializeField]
    private GameObject moneyError;
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
            StartCoroutine(Error_Money());
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

            case "Devil":
                PlayerPrefs.SetString("isDevil", "true");
                playerMoney -= productCoin;
                break;

            case "Angel":
                PlayerPrefs.SetString("isAngel", "true");
                playerMoney -= productCoin;
                break;

            case "Melona":
                PlayerPrefs.SetString("isMelona", "true");
                playerMoney -= productCoin;
                break;
        }

        PlayerPrefs.SetInt("COIN", playerMoney);
        ShopSoundManager.Instance.PaySound();
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
            coinText.text = string.Format("{0}원", productCoin);
            soldShiba = "Strawberry";
        }
    }

    public void OnClickMint()
    {
        if (PlayerPrefs.GetString("isMint") != "true")
        {
            productCoin = 150;
            buyPopup.SetActive(true);
            coinText.text = string.Format("{0}원", productCoin);
            soldShiba = "Mint";
        }
    }

    public void OnClickDevil()
    {
        if (PlayerPrefs.GetString("isDevil") != "true")
        {
            productCoin = 170;
            buyPopup.SetActive(true);
            coinText.text = string.Format("{0}원", productCoin);
            soldShiba = "Devil";
        }
    }

    public void OnClickAngel()
    {
        if (PlayerPrefs.GetString("isAngel") != "true")
        {
            productCoin = 100;
            buyPopup.SetActive(true);
            coinText.text = string.Format("{0}원", productCoin);
            soldShiba = "Angel";
        }
    }

    public void OnClickMelona()
    {
        if (PlayerPrefs.GetString("isMelona") != "true")
        {
            productCoin = 180;
            buyPopup.SetActive(true);
            coinText.text = string.Format("{0}원", productCoin);
            soldShiba = "Melona";
        }
    }
    IEnumerator Error_Money()
    {
        ShopSoundManager.Instance.ErrorSound();
        moneyError.SetActive(true);
        yield return new WaitForSeconds(1f);
        moneyError.SetActive(false);
        yield break;
    }

    private void ChangeSprite()
    {
        for(int i = 0; i< contents.transform.childCount; i++)
        {
            contents.transform.GetChild(i).GetComponent<Skins>().ChangeSprite();
        }
    }
}
