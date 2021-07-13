using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    ShopSoundManager shopSoundManager;

    void Start()
    {
        shopSoundManager = FindObjectOfType<ShopSoundManager>();
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
        buyPopup.transform.DOScale(Vector2.zero, 0.2f).SetEase(Ease.InOutQuad);
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
        shopSoundManager.PaySound();
        UpdateUI();
        ChangeSprite();
        buyPopup.transform.DOScale(Vector2.zero, 0.2f).SetEase(Ease.InOutQuad);
    }

    public void OnClickStrawberry()
    {
        if(PlayerPrefs.GetString("isBuyStrawberry") != "true")
        {
            productCoin = 100;
            buyPopup.transform.DOScale(Vector2.one, 0.2f).SetEase(Ease.InOutQuad);
            coinText.text = string.Format("{0}원", productCoin);
            soldShiba = "Strawberry";
        }
    }

    public void OnClickMint()
    {
        if (PlayerPrefs.GetString("isMint") != "true")
        {
            productCoin = 150;
            buyPopup.transform.DOScale(Vector2.one, 0.2f).SetEase(Ease.InOutQuad);
            coinText.text = string.Format("{0}원", productCoin);
            soldShiba = "Mint";
        }
    }

    public void OnClickDevil()
    {
        if (PlayerPrefs.GetString("isDevil") != "true")
        {
            productCoin = 170;
            buyPopup.transform.DOScale(Vector2.one, 0.2f).SetEase(Ease.InOutQuad);
            coinText.text = string.Format("{0}원", productCoin);
            soldShiba = "Devil";
        }
    }

    public void OnClickAngel()
    {
        if (PlayerPrefs.GetString("isAngel") != "true")
        {
            productCoin = 100;
            buyPopup.transform.DOScale(Vector2.one, 0.2f).SetEase(Ease.InOutQuad);
            coinText.text = string.Format("{0}원", productCoin);
            soldShiba = "Angel";
        }
    }

    public void OnClickMelona()
    {
        if (PlayerPrefs.GetString("isMelona") != "true")
        {
            productCoin = 180;
            buyPopup.transform.DOScale(Vector2.one, 0.2f).SetEase(Ease.InOutQuad);
            coinText.text = string.Format("{0}원", productCoin);
            soldShiba = "Melona";
        }
    }

    IEnumerator Error_Money()
    {
        shopSoundManager.ErrorSound();
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
