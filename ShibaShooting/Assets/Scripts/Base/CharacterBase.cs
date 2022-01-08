using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer spriteRenderer = null;
    [HideInInspector] public Animator animator = null;
    public float speed = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
}
