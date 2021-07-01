using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    GameObject parent;
    [SerializeField]
    GameObject ItemPopup, buyButton, coinImage, moneyError, levelError;
    [SerializeField]
    Text itemName, itemLevel;
    [SerializeField]
    Text itemInfo, coinText, productCoinText;
    [SerializeField]
    Image itemImage;
    [SerializeField]
    GameObject itemField, skinField;

    [SerializeField]
    Sprite big, small, slow, coin, life, lightning, tired;

    private float bigTime, smallTime, slowTime, tiredTime;
    private int bigCoin, smallCoin, coinCoin, slowCoin, tiredCoin;
    private int bigLevel, smallLevel, slowLevel, coinLevel, tiredLevel, coinAmount;

    private int playerCoin;

    private string crtitemName;

    private void Start()
    {
        playerCoin = PlayerPrefs.GetInt("COIN");
        coinText.text = string.Format("{0}", playerCoin);
        SetPlayerPrefs();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerCoin += 40;
            PlayerPrefs.SetInt("COIN", playerCoin);
            coinText.text = string.Format("{0}", playerCoin);
        }
    }

    #region 버튼 함수들
    public void OnClickBigItem()
    {
        ItemPopup.SetActive(true);
        Information("BigItem");
        crtitemName = "BigItem";
        buyButton.SetActive(true);
        coinImage.SetActive(true);
    }

    public void OnClickSmallItem()
    {
        ItemPopup.SetActive(true);
        Information("SmallItem");
        crtitemName = "SmallItem";
        buyButton.SetActive(true);
        coinImage.SetActive(true);
    }

    public void OnClickSlowItem()
    {
        ItemPopup.SetActive(true);
        Information("SlowItem");
        crtitemName = "SlowItem";
        buyButton.SetActive(true);
        coinImage.SetActive(true);
    }

    public void OnClickCoinItem()
    {
        ItemPopup.SetActive(true);
        Information("CoinItem");
        crtitemName = "CoinItem";
        buyButton.SetActive(true);
        coinImage.SetActive(true);
    }

    public void OnClickLifeItem()
    {
        ItemPopup.SetActive(true);
        Information("LifeItem");
        crtitemName = "LifeItem";
        buyButton.SetActive(false);
        coinImage.SetActive(false);
    }

    public void OnClickLightningItem()
    {
        ItemPopup.SetActive(true);
        Information("LightningItem");
        crtitemName = "LightningItem";
        buyButton.SetActive(false);
        coinImage.SetActive(false);
    }

    public void OnClickTiredItem()
    {
        ItemPopup.SetActive(true);
        Information("TiredItem");
        crtitemName = "TiredItem";
        buyButton.SetActive(true);
        coinImage.SetActive(true);
    }

    public void InfoActiveFalse()
    {
        ItemPopup.SetActive(false);
    }
    #endregion

    private void Information(string name)
    {
        coinText.text = string.Format("{0}", PlayerPrefs.GetInt("COIN"));
        SetPlayerPrefs();

        switch (name)
        {
            case "BigItem":
                itemImage.sprite = big;
                itemLevel.text = string.Format("{0} Level", bigLevel);
                itemName.text = string.Format("시바쌤은 파괴신");
                itemInfo.text = string.Format("{0:N1}초 동안 커진 몸상태로 모든 총알을 파괴하고 발사하는 유니티의 데미지가 3배 증가합니다.", bigTime);
                productCoinText.text = string.Format("{0}", bigCoin);
                break;

            case "SmallItem":
                itemImage.sprite = small;
                itemLevel.text = string.Format("{0} Level", smallLevel);
                itemName.text = string.Format("작아진 시바쌤");
                itemInfo.text = string.Format("{0:N1}초 동안 작아진 몸상태가 됩니다. 총알을 무시할 수 없지만 학생들의 짜증을 피하기가 더 수월해집니다.", smallTime);
                productCoinText.text = string.Format("{0}", smallCoin);
                break;

            case "SlowItem":
                itemImage.sprite = slow;
                itemLevel.text = string.Format("{0} Level", slowLevel);
                itemName.text = string.Format("시간끌기");
                itemInfo.text = string.Format("{0:N1}초 동안 시간이 2배 느려집니다. 이동 속도는 느려지지 않아 학생들의 짜증을 피하기 수월해집니다.", slowTime);
                productCoinText.text = string.Format("{0}", slowCoin);
                break;

            case "CoinItem":
                itemImage.sprite = coin;
                itemLevel.text = string.Format("{0} Level", coinLevel);
                itemName.text = string.Format("금융치료");
                itemInfo.text = string.Format("즉시 {0}코인 지갑에 들어옵니다. 아무 변화가 없지만 괜스레 힘이 납니다.", coinAmount);
                productCoinText.text = string.Format("{0}", coinCoin);
                break;

            case "LifeItem":
                itemImage.sprite = life;
                itemName.text = string.Format("얘들아 사랑한다 ^^...");
                itemInfo.text = string.Format("생명이 하나 늘어납니다. 사랑하는 학생들에게 유니티를 발사할 기회를 늘려주네요.");
                productCoinText.text = string.Format("");
                break;

            case "LightningItem":
                itemImage.sprite = lightning;
                itemName.text = string.Format("쌤유니티진짜으악");
                itemInfo.text = string.Format("학생들의 유니티를 향한 분노가 번개가 됩니다. 맞으면 아프니 조심하세요.");
                productCoinText.text = string.Format("");
                break;

            case "TiredItem":
                itemImage.sprite = tired;
                itemLevel.text = string.Format("{0} Level", tiredLevel);
                itemName.text = string.Format("얘들아 왜 다 전멸이야");
                itemInfo.text = string.Format("학생들이 모두 졸고 있습니다. 당신도 {0:N1}초 동안 눈이 감겨 이동 속도가 둔해집니다.", tiredTime);
                productCoinText.text = string.Format("{0}", tiredCoin);
                break;
        }
    }

    public void OnClickLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    private void SetPlayerPrefs()
    {
        bigTime = PlayerPrefs.GetFloat("bigTime", 5);
        smallTime = PlayerPrefs.GetFloat("smallTime", 5);
        slowTime = PlayerPrefs.GetFloat("slowTime", 5);
        coinAmount = PlayerPrefs.GetInt("coinAmount", 5);
        tiredTime = PlayerPrefs.GetFloat("tiredTime", 7);

        bigLevel = PlayerPrefs.GetInt("bigLevel", 1);
        smallLevel = PlayerPrefs.GetInt("smallLevel", 1);
        slowLevel = PlayerPrefs.GetInt("slowLevel", 1);
        coinLevel = PlayerPrefs.GetInt("coinLevel", 1);
        tiredLevel = PlayerPrefs.GetInt("tiredLevel", 1);

        bigCoin = PlayerPrefs.GetInt("bigCoin", 25);
        smallCoin = PlayerPrefs.GetInt("smallCoin", 25);
        slowCoin = PlayerPrefs.GetInt("slowCoin", 25);
        coinCoin = PlayerPrefs.GetInt("coinCoin", 25);
        tiredCoin = PlayerPrefs.GetInt("tiredCoin", 25);
    }

    public void OnClickBuy()
    {
        Debug.Log(crtitemName);

        switch (crtitemName)
        {
            case "BigItem":
                if (MaxLevel(bigLevel))
                {
                    StartCoroutine(Error_Level());
                    return;
                }
                if (playerCoin - bigCoin < 0)
                {
                    StartCoroutine(Error_Money());
                    return;
                }

                playerCoin -= bigCoin;
                bigLevel++;
                bigTime += 0.2f;
                bigCoin += 10;
                PlayerPrefs.SetFloat("bigTime", bigTime);
                PlayerPrefs.SetInt("bigLevel", bigLevel);
                PlayerPrefs.SetInt("bigCoin", bigCoin);
                Information("BigItem");
                break;

            case "SmallItem":
                if (MaxLevel(smallLevel))
                {
                    StartCoroutine(Error_Level());
                    return;
                }
                if (playerCoin - smallCoin < 0)
                {
                    StartCoroutine(Error_Money());
                    return;
                }

                playerCoin -= smallCoin;

                smallLevel++;
                smallTime += 0.2f;
                smallCoin += 10;

                PlayerPrefs.SetFloat("smallTime", smallTime);
                PlayerPrefs.SetInt("smallLevel", smallLevel);
                PlayerPrefs.SetInt("smallCoin", smallCoin);
                Information("SmallItem");
                break;

            case "SlowItem":
                if (MaxLevel(slowLevel))
                {
                    StartCoroutine(Error_Level());
                    return;
                }
                if (playerCoin - slowCoin < 0)
                {
                    StartCoroutine(Error_Money());
                    return;
                }

                playerCoin -= slowCoin;

                slowLevel++;
                slowTime += 0.2f;
                slowCoin += 10;
                PlayerPrefs.SetFloat("slowTime", slowTime);
                PlayerPrefs.SetInt("slowLevel", slowLevel);
                PlayerPrefs.SetInt("slowCoin", slowCoin);
                Information("SlowItem");
                break;

            case "CoinItem":
                if (MaxLevel(coinLevel))
                {
                    StartCoroutine(Error_Level());
                    return;
                }
                if (playerCoin - coinCoin < 0)
                {
                    StartCoroutine(Error_Money());
                    return;
                }
                
                playerCoin -= coinCoin;

                coinLevel++;
                coinAmount += 2;
                coinCoin += 10;

                PlayerPrefs.SetInt("coinAmount", coinAmount);
                PlayerPrefs.SetInt("coinLevel", coinLevel);
                PlayerPrefs.SetInt("coinCoin", coinCoin);
                Information("CoinItem");
                break;

            case "TiredItem":
                if (MaxLevel(tiredLevel))
                {
                    StartCoroutine(Error_Level());
                    return;
                }
                if (playerCoin - tiredCoin < 0)
                {
                    StartCoroutine(Error_Money());
                    return;
                }
                

                playerCoin -= tiredCoin;

                tiredLevel++;
                tiredTime -= 0.2f;
                tiredCoin += 10;

                PlayerPrefs.SetFloat("tiredTime", tiredTime);
                PlayerPrefs.SetInt("tiredLevel", tiredLevel);
                PlayerPrefs.SetInt("tiredCoin", tiredCoin);
                Information("TiredItem");
                break;
        }

        PlayerPrefs.SetInt("COIN", playerCoin);
        coinText.text = string.Format("{0}", playerCoin);
    }

    private bool MaxLevel(int level)
    {
        if (level > 10) return true;
        else return false;
    }

    IEnumerator Error_Money()
    {
        moneyError.SetActive(true);
        yield return new WaitForSeconds(1f);
        moneyError.SetActive(false);
        yield break;
    }

    IEnumerator Error_Level()
    {
        levelError.SetActive(true);
        yield return new WaitForSeconds(1f);
        levelError.SetActive(false);
        yield break;
    }

    public void OnClickItem()
    {
        itemField.SetActive(true);
        skinField.SetActive(false);
    }

    public void OnClickSkin()
    {
        itemField.SetActive(false);
        skinField.SetActive(true);
    }
}
