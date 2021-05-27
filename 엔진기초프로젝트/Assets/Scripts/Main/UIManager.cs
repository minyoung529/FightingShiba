using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("생명")]
    [SerializeField]
    private GameObject heart1 = null;
    [SerializeField]
    private GameObject heart2 = null;
    [SerializeField]
    private GameObject heart3 = null;

    [Header("게임 중지 팝업")]
    [SerializeField]
    private GameObject stopPopUp = null;

    [Header("딜레이 타임 텍스트")]
    [SerializeField]
    private Text textDelayTime = null;


    private GameManager gameManager = null;

    private int time = 3;
    private float delayTime = 3f;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void destroyHeart()
    {
        if (gameManager.life == 2)
            heart3.SetActive(false);

        else if (gameManager.life == 1)
            heart2.SetActive(false);

        else if (gameManager.life == 0)
            heart1.SetActive(false);
    }


    public void OnClickStop()
    {
        gameManager.StopGame();
        stopPopUp.SetActive(true);
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnClickNewGame()
    {
        SceneManager.LoadScene("Main");
        stopPopUp.SetActive(false);
    }

    public void OnClickContinue()
    {
        stopPopUp.SetActive(false);
        //StartCoroutine(Delay());
        gameManager.ContinueGame();
        StartCoroutine(continuedelay());
    }

    //private IEnumerator Delay()
    //{
        
    //    //Debug.Log(delayTime);
    //    //while(true)
    //    //{
    //    //    delayTime -= Time.deltaTime;

    //    //    if (delayTime == 3)
    //    //    {
    //    //        time = 3;
    //    //        textDelayTime.text = string.Format("{0}", time);
    //    //    }

    //    //    else if (delayTime == 2)
    //    //    {
    //    //        time = 2;
    //    //        textDelayTime.text = string.Format("{0}", time);
    //    //    }

    //    //    else if (delayTime == 1)
    //    //    {
    //    //        time = 1;
    //    //        textDelayTime.text = string.Format("{0}", time);
    //    //    }
    //    //    if (delayTime <= 0)
    //    //        yield return 0;
    //    //}
        
    //}
    private IEnumerator continuedelay()
    {
        while (time > 0)
        {
            textDelayTime.text = string.Format("{0}", time);
            Debug.Log(time);
            time--;
            yield return new WaitForSeconds(1f);
        }
    }
}
