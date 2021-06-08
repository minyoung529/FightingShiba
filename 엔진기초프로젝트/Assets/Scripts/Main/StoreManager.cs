using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour
{
    private GameManager gameManager = null;

    [SerializeField]
    private Text coinText;
    void Start()
    {
        SetCoinText();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetCoinText()
    {
        coinText.text = string.Format("{0}", PlayerPrefs.GetInt("COIN"));
    }

    public void OnClickLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void BigItemUpgrade()
    {
        int coin = PlayerPrefs.GetInt("COIN");

        if (coin < 100)
        {
            Debug.Log("잔액이 부족합니다.");
            return;
        }
        coin -= 100;
    }
}
