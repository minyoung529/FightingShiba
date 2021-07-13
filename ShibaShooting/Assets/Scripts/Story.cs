using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Story : MonoBehaviour
{
    [SerializeField] private Image s1, s2, s3, s4, s5, s6;
    private float delayTime = 3;
    private Image[] stories;

    void Start()
    {
        StartCoroutine(StoryTelling());
    }

    private void SetupStories()
    {
        stories = new Image[6];
        stories[0] = s1;
        stories[1] = s2;
        stories[2] = s3;
        stories[3] = s4;
        stories[4] = s5;
        stories[5] = s6;
    }

    private IEnumerator StoryTelling()
    {
        SetupStories();

        if(PlayerPrefs.GetString("V", "true") == "true")
        {
            StartCoroutine(Vibrate());
        }

        yield return new WaitForSeconds(delayTime);

        for (int i = 0; i<stories.Length; i++)
        {
            if(i==stories.Length)
            {
                break;
            }

            stories[i].DOFade(0, 0.25f);
            yield return new WaitForSeconds(delayTime);
        }

        SceneManager.LoadScene("Main");
    }

    private IEnumerator Vibrate()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Handheld.Vibrate();
        }
    }
    public void OnClickSkip()
    {
        SceneManager.LoadScene("Main");
    }
}
