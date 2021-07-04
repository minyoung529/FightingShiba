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
    private GameObject infoPopup;
    [SerializeField]
    private GameObject resetPopup, resetOKPopup;
    [SerializeField]
    private Slider volumeController;
    [SerializeField]
    private AudioSource backgroundMusic;

    private bool isInfo;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitPopUp.gameObject.SetActive(true);
        }
    }

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

    public void OnClickInfo()
    {
        if (!isInfo)
        {
            isInfo = true;
            infoPopup.SetActive(true);
        }

        else
        {
            isInfo = false;
            infoPopup.SetActive(false);
        }
    }

    public void ReTutorial()
    {
        PlayerPrefs.SetString("First", "true");
        SceneManager.LoadScene("Main");
    }

    public void CheckReset()
    {
        resetPopup.SetActive(true);
    }

    public void ResetNO()
    {
        resetPopup.SetActive(false);
    }

    public void AllReset()
    {
        PlayerPrefs.DeleteAll();
        resetPopup.SetActive(false);
        settingPopup.SetActive(false);
        StartCoroutine(Error_Money());
    }

    IEnumerator Error_Money()
    {
        resetOKPopup.SetActive(true);
        yield return new WaitForSeconds(1f);
        resetOKPopup.SetActive(false);
        yield break;
    }
}
