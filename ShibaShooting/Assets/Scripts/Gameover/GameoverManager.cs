using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverManager : MonoBehaviour
{
    [SerializeField]
    private Text highScoreText;

    private void Start()
    {
        highScoreText.text = string.Format("HIGHSCORE\n{0}", PlayerPrefs.GetInt("HIGHSCORE"));
    }
    public void OnClickStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
