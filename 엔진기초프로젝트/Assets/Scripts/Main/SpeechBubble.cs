using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    [Header("말풍선 스프라이트")]
    [SerializeField]
    private Sprite speechBuble_1_1;
    [SerializeField]
    private Sprite speechBuble_1_2;
    [SerializeField]
    private Sprite speechBuble_2_1;
    [SerializeField]
    private Sprite speechBuble_2_2;
    [SerializeField]
    private Sprite speechBuble_3_1;
    [SerializeField]
    private Sprite speechBuble_3_2;
    [SerializeField]
    private Sprite speechBuble_4_1;
    [SerializeField]
    private Sprite speechBuble_4_2;

    private SpriteRenderer spriteRenderer = null;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    public void ChangeSprites(int a)
    {
        switch(a)
        {
            case 1:
                spriteRenderer.sprite = speechBuble_1_1;
                break;

            case 2:
                spriteRenderer.sprite = speechBuble_1_2;
                break;

            case 3:
                spriteRenderer.sprite = speechBuble_2_1;
                break;

            case 4:
                spriteRenderer.sprite = speechBuble_2_2;
                break;

            case 5:
                spriteRenderer.sprite = speechBuble_3_1;
                break;

            case 6:
                spriteRenderer.sprite = speechBuble_3_2;
                break;

            case 7:
                spriteRenderer.sprite = speechBuble_4_1;
                break;

            case 8:
                spriteRenderer.sprite = speechBuble_4_2;
                break;
        }
    }
}
