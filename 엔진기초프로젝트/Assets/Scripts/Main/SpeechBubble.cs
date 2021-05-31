using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [Header("��ǳ�� ��������Ʈ")]
    [SerializeField]
    private Sprite speechBuble_UnityThrow;

    private SpriteRenderer spriteRenderer = null;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeToUnityTrow()
    {
        spriteRenderer.sprite = speechBuble_UnityThrow;
    }
}
