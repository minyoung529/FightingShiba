using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

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
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip infoSound, buttonSound;

    private bool isInfo, isSetting, isCheckReset, isQuit;

    [SerializeField]
    Text vibrate;
    [SerializeField]
    Image vibButton;

    private void Start()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isCheckReset)
        {
            resetPopup.SetActive(false);
            isCheckReset = false;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && isSetting && !isCheckReset)
        {
            QuitSetting();
            isCheckReset = false;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && !isCheckReset && !isSetting && !isQuit)
        {
            isQuit = true;
            QuitPopUp.gameObject.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && !isCheckReset && !isSetting && isQuit)
        {
            QuitPopUp.gameObject.SetActive(false);
            isQuit = false;
        }
    }

    public void OnClickStart()
    {
        audioSource.PlayOneShot(buttonSound);

        if(PlayerPrefs.GetString("First", "true") == "true")
        {
            SceneManager.LoadScene("Story");
            return;
        }

        else
        {
            SceneManager.LoadScene("Main");
        }
    }

    public void OnClickVibrate_On()
    {
        vibButton.color = Color.white;
        vibrate.text = string.Format("진동 끄기");
        PlayerPrefs.SetString("V", "true");
    }

    public void OnClickVibrate_Off()
    {
        vibButton.color = Color.black;
        vibrate.text = string.Format("진동 켜기");
        PlayerPrefs.SetString("V", "false");
    }

    public void OnClickVibrate()
    {
        if (PlayerPrefs.GetString("V", "true") == "true")
        {
            OnClickVibrate_Off();
        }

        else
        {
            OnClickVibrate_On();
        }
    }

    private void SetVib()
    {
        if (PlayerPrefs.GetString("V", "true") == "true")
        {
            OnClickVibrate_On();
        }

        else
        {
            OnClickVibrate_Off();
        }
    }

    public void OnClickCheckQuit()
    {
        audioSource.PlayOneShot(buttonSound);
        isQuit = true;
        QuitPopUp.SetActive(true);
    }

    public void Quit()
    {
        audioSource.PlayOneShot(buttonSound);
        Application.Quit();
        isQuit = false;
    }

    public void NotQuit()
    {
        isQuit = false;
        QuitPopUp.SetActive(false);
    }

    public void OnClickItem()
    {
        audioSource.PlayOneShot(buttonSound);
        SceneManager.LoadScene("Item");
    }

    public void SetMusicVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void OnClickSetting()
    {
        SetVib();
        audioSource.PlayOneShot(infoSound);

        if (isSetting)
        {
            QuitSetting();
            isSetting = false;
            return;
        }

        isSetting = true;
        settingPopup.transform.DOScale(Vector2.one, 0.2f).SetEase(Ease.InOutQuad);
    }

    public void QuitSetting()
    {
        isSetting = false;
        settingPopup.transform.DOScale(Vector2.zero, 0.2f).SetEase(Ease.InOutQuad);
    }

    public void OnClickInfo()
    {
        audioSource.PlayOneShot(infoSound);
        if (!isInfo)
        {
            isInfo = true;
            infoPopup.transform.DOScale(Vector2.one, 0.2f).SetEase(Ease.InOutQuad);
        }

        else
        {
            isInfo = false;
            infoPopup.transform.DOScale(Vector2.zero, 0.2f).SetEase(Ease.InOutQuad);
        }
    }

    public void ReTutorial()
    {
        isSetting = false;
        PlayerPrefs.SetString("First", "true");
        SceneManager.LoadScene("Story");
    }

    public void CheckReset()
    {
        isCheckReset = true;
        resetPopup.SetActive(true);
    }

    public void ResetNO()
    {
        isCheckReset = false;
        isSetting = false;
        resetPopup.SetActive(false);
    }

    public void AllReset()
    {
        isCheckReset = false;
        isSetting = false;
        PlayerPrefs.DeleteAll();
        resetPopup.SetActive(false);
        settingPopup.transform.DOScale(Vector2.zero, 0.2f).SetEase(Ease.InOutQuad);
        StartCoroutine(ResetFinish());
    }

    IEnumerator ResetFinish()
    {
        resetOKPopup.SetActive(true);
        yield return new WaitForSeconds(2f);
        resetOKPopup.SetActive(false);
        yield break;
    }
}
