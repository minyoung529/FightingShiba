using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverManager : MonoBehaviour
{
    public void OnClickStart()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
