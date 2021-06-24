using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    GameObject parent;
    [SerializeField]
    GameObject ItemPopup;
    [SerializeField]
    Text itemName;
    [SerializeField]
    Text itemInfo;
    [SerializeField]
    Image itemImage;

    [SerializeField]
    Sprite big, small, slow, coin, life, lightning, tired;

    #region ��ư �Լ���
    public void OnClickBigItem()
    {
        ItemPopup.SetActive(true);
        Information("BigItem");
    }

    public void OnClickSmallItem()
    {
        ItemPopup.SetActive(true);
        Information("SmallItem");
    }

    public void OnClickSlowItem()
    {
        ItemPopup.SetActive(true);
        Information("SlowItem");
    }

    public void OnClickCoinItem()
    {
        ItemPopup.SetActive(true);
        Information("CoinItem");
    }

    public void OnClickLifeItem()
    {
        ItemPopup.SetActive(true);
        Information("LifeItem");
    }

    public void OnClickLightningItem()
    {
        ItemPopup.SetActive(true);
        Information("LightningItem");
    }

    public void OnClickTiredItem()
    {
        ItemPopup.SetActive(true);
        Information("TiredItem");
    }

    public void InfoActiveFalse()
    {
        ItemPopup.SetActive(false);
    }
    #endregion

    private void Information(string name)
    {
        switch (name)
        {
            case "BigItem":
                itemImage.sprite = big;
                itemName.text = string.Format("�ùٽ��� �ı���");
                itemInfo.text = string.Format("5�� ���� Ŀ�� �����·� ��� �Ѿ��� �ı��ϰ� �߻��ϴ� ����Ƽ�� �������� 3�� �����մϴ�.");
                break;

            case "SmallItem":
                itemImage.sprite = small;
                itemName.text = string.Format("�۾��� �ùٽ�");
                itemInfo.text = string.Format("5�� ���� �۾��� �����°� �˴ϴ�. �Ѿ��� ������ �� ������ �л����� ¥���� ���ϱⰡ �� ���������ϴ�.");
                break;

            case "SlowItem":
                itemImage.sprite = slow;
                itemName.text = string.Format("�ð�����");
                itemInfo.text = string.Format("6�� ���� �ð��� 2�� �������ϴ�. �̵� �ӵ��� �������� �ʾ� �л����� ¥���� ���ϱ� ���������ϴ�.");
                break;

            case "CoinItem":
                itemImage.sprite = coin;
                itemName.text = string.Format("����ġ��");
                itemInfo.text = string.Format("��� 5���� ������ ���ɴϴ�. �ƹ� ��ȭ�� ������ ������ ���� ���ϴ�.");
                break;

            case "LifeItem":
                itemImage.sprite = life;
                itemName.text = string.Format("���� ����Ѵ� ^^...");
                itemInfo.text = string.Format("������ �ϳ� �þ�ϴ�. ����ϴ� �л��鿡�� ����Ƽ�� �߻��� ��ȸ�� �÷��ֳ׿�.");
                break;

            case "LightningItem":
                itemImage.sprite = lightning;
                itemName.text = string.Format("������Ƽ��¥����");
                itemInfo.text = string.Format("�л����� ����Ƽ�� ���� �г밡 ������ �˴ϴ�. ������ ������ �����ϼ���.");
                break;

            case "TiredItem":
                itemImage.sprite = tired;
                itemName.text = string.Format("���� �� �� �����̾�");
                itemInfo.text = string.Format("�л����� ��� ���� �ֽ��ϴ�. ��ŵ� 7�� ���� ���� ���� �̵� �ӵ��� �������ϴ�.");
                break;
        }
    }
}
