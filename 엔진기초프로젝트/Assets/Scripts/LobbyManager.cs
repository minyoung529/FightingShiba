using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject QuitPopUp = null;

    private void Update()
    {
        if(QuitPopUp.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            QuitPopUp.SetActive(false);
        }

        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            QuitPopUp.SetActive(true);
        }
    }

    public void OnClickStart()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClickCheckQuit()
    {
        QuitPopUp.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
