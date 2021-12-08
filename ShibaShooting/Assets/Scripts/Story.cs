using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Story : MonoBehaviour
{
    private float delayTime = 3;
    [SerializeField]
    private Image[] stories;

    private void OnEnable()
    {
        StartCoroutine(StoryTelling());
    }

    private IEnumerator StoryTelling()
    {
        if (GameManager.Instance.CurrentUser.isVibrate)
        {
            StartCoroutine(Vibrate());
        }

        yield return new WaitForSeconds(delayTime);

        for (int i = 0; i < stories.Length; i++)
        {
            stories[i].DOFade(0, 0.25f);
            yield return new WaitForSeconds(delayTime);
        }

        GameManager.Instance.UIManager.GoToScene(SceneType.Main);
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
        GameManager.Instance.UIManager.GoToScene(SceneType.Main);
    }
}
