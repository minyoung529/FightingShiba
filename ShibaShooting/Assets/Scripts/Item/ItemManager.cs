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
    GameObject ItemPopup;
    [SerializeField]
    Text itemName;
    [SerializeField]
    Text itemInfo;
    [SerializeField]
    Image itemImage;

    [SerializeField]
    Sprite big, small, slow, coin, life, lightning, tired;

    #region 버튼 함수들
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
                itemName.text = string.Format("시바쌤은 파괴신");
                itemInfo.text = string.Format("5초 동안 커진 몸상태로 모든 총알을 파괴하고 발사하는 유니티의 데미지가 3배 증가합니다.");
                break;

            case "SmallItem":
                itemImage.sprite = small;
                itemName.text = string.Format("작아진 시바쌤");
                itemInfo.text = string.Format("5초 동안 작아진 몸상태가 됩니다. 총알을 무시할 수 없지만 학생들의 짜증을 피하기가 더 수월해집니다.");
                break;

            case "SlowItem":
                itemImage.sprite = slow;
                itemName.text = string.Format("시간끌기");
                itemInfo.text = string.Format("6초 동안 시간이 2배 느려집니다. 이동 속도는 느려지지 않아 학생들의 짜증을 피하기 수월해집니다.");
                break;

            case "CoinItem":
                itemImage.sprite = coin;
                itemName.text = string.Format("금융치료");
                itemInfo.text = string.Format("즉시 5코인 지갑에 들어옵니다. 아무 변화가 없지만 괜스레 힘이 납니다.");
                break;

            case "LifeItem":
                itemImage.sprite = life;
                itemName.text = string.Format("얘들아 사랑한다 ^^...");
                itemInfo.text = string.Format("생명이 하나 늘어납니다. 사랑하는 학생들에게 유니티를 발사할 기회를 늘려주네요.");
                break;

            case "LightningItem":
                itemImage.sprite = lightning;
                itemName.text = string.Format("쌤유니티진짜으악");
                itemInfo.text = string.Format("학생들의 유니티를 향한 분노가 번개가 됩니다. 맞으면 아프니 조심하세요.");
                break;

            case "TiredItem":
                itemImage.sprite = tired;
                itemName.text = string.Format("얘들아 왜 다 전멸이야");
                itemInfo.text = string.Format("학생들이 모두 졸고 있습니다. 당신도 7초 동안 눈이 감겨 이동 속도가 둔해집니다.");
                break;
        }
    }

    public void OnClickLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
