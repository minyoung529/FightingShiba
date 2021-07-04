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

    [SerializeField]
    Button itemBtn, skinBtn;

    private float bigTime, smallTime, slowTime, tiredTime;
    private int bigCoin, smallCoin, coinCoin, slowCoin, tiredCoin;
    private int bigLevel, smallLevel, slowLevel, coinLevel, tiredLevel, coinAmount;

    private int playerCoin;

    private string crtitemName;
    private string isFirst;
    private int cnt;

    [SerializeField]
    private Text characterName, characterSpeech;
    [SerializeField]
    private GameObject textBox, shiba, storeOwner;

    private void Start()
    {
        itemBtn.image.color = new Color32(255, 229, 95, 255);

        playerCoin = PlayerPrefs.GetInt("COIN");
        coinText.text = string.Format("{0}", playerCoin);
        SetPlayerPrefs();

        isFirst = PlayerPrefs.GetString("StoreFirst", "true");

        if (isFirst == "true")
        {
            textBox.SetActive(true);
            storeOwner.SetActive(true);
            shiba.SetActive(false);
            CharacterText("???", "어서오세요!");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Lobby");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerCoin += 40;
            PlayerPrefs.SetInt("COIN", playerCoin);
            coinText.text = string.Format("{0}", playerCoin);
        }

        if (Input.GetMouseButtonDown(0) && textBox.activeSelf)
        {
            cnt++;
            StoreTutorial();
        }
    }

    #region 버튼 함수들
    public void OnClickBigItem()
    {
        if (isFirst == "true") return;
        ItemPopup.SetActive(true);
        Information("BigItem");
        crtitemName = "BigItem";
        buyButton.SetActive(true);
        coinImage.SetActive(true);
    }

    public void OnClickSmallItem()
    {
        if (isFirst == "true") return;
        ItemPopup.SetActive(true);
        Information("SmallItem");
        crtitemName = "SmallItem";
        buyButton.SetActive(true);
        coinImage.SetActive(true);
    }

    public void OnClickSlowItem()
    {
        if (isFirst == "true") return;
        ItemPopup.SetActive(true);
        Information("SlowItem");
        crtitemName = "SlowItem";
        buyButton.SetActive(true);
        coinImage.SetActive(true);
    }

    public void OnClickCoinItem()
    {
        if (isFirst == "true") return;
        ItemPopup.SetActive(true);
        Information("CoinItem");
        crtitemName = "CoinItem";
        buyButton.SetActive(true);
        coinImage.SetActive(true);
    }

    public void OnClickLifeItem()
    {
        if (isFirst == "true") return;
        ItemPopup.SetActive(true);
        Information("LifeItem");
        crtitemName = "LifeItem";
        buyButton.SetActive(false);
        coinImage.SetActive(false);
    }

    public void OnClickLightningItem()
    {
        if (isFirst == "true") return;
        ItemPopup.SetActive(true);
        Information("LightningItem");
        crtitemName = "LightningItem";
        buyButton.SetActive(false);
        coinImage.SetActive(false);
    }

    public void OnClickTiredItem()
    {
        if (isFirst == "true") return;
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
        if (isFirst == "true") return;
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
        ShopSoundManager.Instance.ButtonSound();
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
        if (isFirst == "true") return;

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

        ShopSoundManager.Instance.PaySound();
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
        ShopSoundManager.Instance.ErrorSound();
        moneyError.SetActive(true);
        yield return new WaitForSeconds(1f);
        moneyError.SetActive(false);
        yield break;
    }

    IEnumerator Error_Level()
    {
        ShopSoundManager.Instance.ErrorSound();
        levelError.SetActive(true);
        yield return new WaitForSeconds(1f);
        levelError.SetActive(false);
        yield break;
    }

    public void OnClickItem()
    {
        if (isFirst == "true") return;
        ShopSoundManager.Instance.ButtonSound();
        itemField.SetActive(true);
        skinField.SetActive(false);

        itemBtn.image.color = new Color32(255, 229, 95, 255);
        skinBtn.image.color = new Color32(255, 255, 255, 255);

    }

    public void OnClickSkin()
    {
        if (isFirst == "true") return;
        ShopSoundManager.Instance.ButtonSound();

        itemField.SetActive(false);
        skinField.SetActive(true);

        skinBtn.image.color = new Color32(255, 229, 95, 255);
        itemBtn.image.color = new Color32(255, 255, 255, 255);
    }

    private void CharacterText(string charName, string charText)
    {
        characterName.text = string.Format(charName);
        characterSpeech.text = string.Format(charText);
    }

    private void StoreTutorial()
    {
        switch (cnt)
        {
            case 1:
                CharacterText("시바선생님", "아 네... 안녕하세요?");
                shiba.SetActive(true);
                storeOwner.SetActive(false);
                break;
            case 2:
                CharacterText("???", "안녕하세요 히히");
                shiba.SetActive(false);
                storeOwner.SetActive(true);
                break;
            case 3:
                CharacterText("꿀곰(상점주인)", "저는 이 상점 주인 꿀곰이에요!");
                break;
            case 4:
                CharacterText("시바선생님", "허허 그렇군요");
                shiba.SetActive(true);
                storeOwner.SetActive(false);
                break;
            case 5:
                CharacterText("꿀곰(상점주인)", "보아하니 저희 가게는 처음이신 것 같은데, 간단히 소개해드릴게요!");
                shiba.SetActive(false);
                storeOwner.SetActive(true);
                break;
            case 6:
                CharacterText("꿀곰(상점주인)", "먼저, 가게에서는 아이템을 업그레이드 하고, 예쁜 옷을 살 수 있어요!");
                break;
            case 7:
                CharacterText("꿀곰(상점주인)", "아이템은 최대 11레벨까지 강화가 가능하고,");
                break;
            case 8:
                CharacterText("꿀곰(상점주인)", "옷은 옷별로 디자인과 가격이 달라서 고르는 재미가 있으실 거예요.");
                break;
            case 9:
                CharacterText("꿀곰(상점주인)", "또, 옷을 입으시려면 구매하시고 옷을 한번 더 클릭해주시면 입어진답니다!");
                break;
            case 10:
                CharacterText("시바선생님", "네, 알겠습니다.");
                shiba.SetActive(true);
                storeOwner.SetActive(false);
                break;
            case 11:
                CharacterText("꿀곰(상점주인)", "오랜만에 오는 손님이시니, 저희 가게 150원 상품권을 드리겠습니다!");
                shiba.SetActive(false);
                storeOwner.SetActive(true);

                playerCoin += 150;
                coinText.text = string.Format("{0}", playerCoin);
                ShopSoundManager.Instance.PaySound();

                break;
            case 12:
                CharacterText("꿀곰(상점주인)", "이걸로 아이템을 강화하거나 예쁜 옷을 사보세요!");
                break;
            case 13:
                CharacterText("꿀곰(상점주인)", "앞으로 많은 이용 부탁드립니다!");
                break;
            case 14:
                textBox.SetActive(false);
                PlayerPrefs.SetString("StoreFirst", "false");
                isFirst = PlayerPrefs.GetString("StoreFirst");
                PlayerPrefs.SetInt("COIN", playerCoin);
                break;
        }
    }
}
