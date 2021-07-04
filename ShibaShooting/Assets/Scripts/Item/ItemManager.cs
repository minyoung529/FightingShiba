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
            CharacterText("???", "�������!");
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

    #region ��ư �Լ���
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
                itemName.text = string.Format("�ùٽ��� �ı���");
                itemInfo.text = string.Format("{0:N1}�� ���� Ŀ�� �����·� ��� �Ѿ��� �ı��ϰ� �߻��ϴ� ����Ƽ�� �������� 3�� �����մϴ�.", bigTime);
                productCoinText.text = string.Format("{0}", bigCoin);
                break;

            case "SmallItem":
                itemImage.sprite = small;
                itemLevel.text = string.Format("{0} Level", smallLevel);
                itemName.text = string.Format("�۾��� �ùٽ�");
                itemInfo.text = string.Format("{0:N1}�� ���� �۾��� �����°� �˴ϴ�. �Ѿ��� ������ �� ������ �л����� ¥���� ���ϱⰡ �� ���������ϴ�.", smallTime);
                productCoinText.text = string.Format("{0}", smallCoin);
                break;

            case "SlowItem":
                itemImage.sprite = slow;
                itemLevel.text = string.Format("{0} Level", slowLevel);
                itemName.text = string.Format("�ð�����");
                itemInfo.text = string.Format("{0:N1}�� ���� �ð��� 2�� �������ϴ�. �̵� �ӵ��� �������� �ʾ� �л����� ¥���� ���ϱ� ���������ϴ�.", slowTime);
                productCoinText.text = string.Format("{0}", slowCoin);
                break;

            case "CoinItem":
                itemImage.sprite = coin;
                itemLevel.text = string.Format("{0} Level", coinLevel);
                itemName.text = string.Format("����ġ��");
                itemInfo.text = string.Format("��� {0}���� ������ ���ɴϴ�. �ƹ� ��ȭ�� ������ ������ ���� ���ϴ�.", coinAmount);
                productCoinText.text = string.Format("{0}", coinCoin);
                break;

            case "LifeItem":
                itemImage.sprite = life;
                itemName.text = string.Format("���� ����Ѵ� ^^...");
                itemInfo.text = string.Format("������ �ϳ� �þ�ϴ�. ����ϴ� �л��鿡�� ����Ƽ�� �߻��� ��ȸ�� �÷��ֳ׿�.");
                productCoinText.text = string.Format("");
                break;

            case "LightningItem":
                itemImage.sprite = lightning;
                itemName.text = string.Format("������Ƽ��¥����");
                itemInfo.text = string.Format("�л����� ����Ƽ�� ���� �г밡 ������ �˴ϴ�. ������ ������ �����ϼ���.");
                productCoinText.text = string.Format("");
                break;

            case "TiredItem":
                itemImage.sprite = tired;
                itemLevel.text = string.Format("{0} Level", tiredLevel);
                itemName.text = string.Format("���� �� �� �����̾�");
                itemInfo.text = string.Format("�л����� ��� ���� �ֽ��ϴ�. ��ŵ� {0:N1}�� ���� ���� ���� �̵� �ӵ��� �������ϴ�.", tiredTime);
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
                CharacterText("�ùټ�����", "�� ��... �ȳ��ϼ���?");
                shiba.SetActive(true);
                storeOwner.SetActive(false);
                break;
            case 2:
                CharacterText("???", "�ȳ��ϼ��� ����");
                shiba.SetActive(false);
                storeOwner.SetActive(true);
                break;
            case 3:
                CharacterText("�ܰ�(��������)", "���� �� ���� ���� �ܰ��̿���!");
                break;
            case 4:
                CharacterText("�ùټ�����", "���� �׷�����");
                shiba.SetActive(true);
                storeOwner.SetActive(false);
                break;
            case 5:
                CharacterText("�ܰ�(��������)", "�����ϴ� ���� ���Դ� ó���̽� �� ������, ������ �Ұ��ص帱�Կ�!");
                shiba.SetActive(false);
                storeOwner.SetActive(true);
                break;
            case 6:
                CharacterText("�ܰ�(��������)", "����, ���Կ����� �������� ���׷��̵� �ϰ�, ���� ���� �� �� �־��!");
                break;
            case 7:
                CharacterText("�ܰ�(��������)", "�������� �ִ� 11�������� ��ȭ�� �����ϰ�,");
                break;
            case 8:
                CharacterText("�ܰ�(��������)", "���� �ʺ��� �����ΰ� ������ �޶� ���� ��̰� ������ �ſ���.");
                break;
            case 9:
                CharacterText("�ܰ�(��������)", "��, ���� �����÷��� �����Ͻð� ���� �ѹ� �� Ŭ�����ֽø� �Ծ�����ϴ�!");
                break;
            case 10:
                CharacterText("�ùټ�����", "��, �˰ڽ��ϴ�.");
                shiba.SetActive(true);
                storeOwner.SetActive(false);
                break;
            case 11:
                CharacterText("�ܰ�(��������)", "�������� ���� �մ��̽ô�, ���� ���� 150�� ��ǰ���� �帮�ڽ��ϴ�!");
                shiba.SetActive(false);
                storeOwner.SetActive(true);

                playerCoin += 150;
                coinText.text = string.Format("{0}", playerCoin);
                ShopSoundManager.Instance.PaySound();

                break;
            case 12:
                CharacterText("�ܰ�(��������)", "�̰ɷ� �������� ��ȭ�ϰų� ���� ���� �纸����!");
                break;
            case 13:
                CharacterText("�ܰ�(��������)", "������ ���� �̿� ��Ź�帳�ϴ�!");
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
