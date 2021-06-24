using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject QuitPopUp;
    [SerializeField]
    private GameObject settingPopup;
    [SerializeField]
    private Slider volumeController;
    [SerializeField]
    private AudioSource backgroundMusic;

    public void OnClickStart()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickCheckQuit()
    {
        QuitPopUp.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NotQuit()
    {
        QuitPopUp.SetActive(false);
    }

    public void OnClickItem()
    {
        SceneManager.LoadScene("Item");
    }

    public void SetMusicVolume(float volume)
    {
        backgroundMusic.volume = volume;
    }

    public void OnClickSetting()
    {
        settingPopup.SetActive(true);
    }

    public void QuitSetting()
    {
        settingPopup.SetActive(false);
    }
}
