using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Camera))]
public class CameraMove : MonoBehaviour
{
    [HideInInspector] public Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public void Shake()
    {
        transform.DOShakePosition(0.5f);
    }
}
