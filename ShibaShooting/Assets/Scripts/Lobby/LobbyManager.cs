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

    public void OnClickVibrate()
    {
        GameManager.Instance.CurrentUser.isVibrate = !GameManager.Instance.CurrentUser.isVibrate;
        SetVibrate();
    }

    private void SetVibrate()
    {
        bool isVibrate = GameManager.Instance.CurrentUser.isVibrate;

        if (isVibrate)
        {
            vibrate.text = string.Format("진동 끄기");
            vibButton.color = Color.green;
        }
        else
        {
            vibrate.text = string.Format("진동 켜기");
            vibButton.color = Color.black;
        }
    }

    public void OnClickCheckQuit()
    {
        isQuit = true;
        QuitPopUp.SetActive(true);
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
