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
    [SerializeField]
    private Image butterfly;
    [SerializeField]
    private Image music;

    public bool isButterfly { get; private set; }
    public bool isMusic { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

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

    private void Insufficient(int _coin)
    {
        int coin = PlayerPrefs.GetInt("COIN");

        if (coin < _coin)
        {
            Debug.Log("잔액이 부족합니다.");
            return;
        }

        else
        {
            coin -= 30;
        }
    }

    public void Butterfly()
    {
        Insufficient(30);

        isButterfly = true;
        SceneManager.LoadScene("Main");
    }

    public void Music()
    {
        isMusic = true;
        SceneManager.LoadScene("Main");
    }
}
