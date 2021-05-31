using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [Header("말풍선 스프라이트")]
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
