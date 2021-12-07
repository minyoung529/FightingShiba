using UnityEngine;
using DG.Tweening;

public class ActiveScale : MonoBehaviour
{
    public void OnActive()
    {
        gameObject.SetActive(true);
        transform.DOScale(1f, 0.3f).OnComplete(() => transform.DOKill());
    }

    public void OnInactive()
    {
        gameObject.SetActive(false);
        transform.localScale = Vector3.zero;
    }

    public void OnToggleActive()
    {
        if(gameObject.activeSelf)
        {
            OnInactive();
        }

        else
        {
            OnActive();
        }
    }
}
