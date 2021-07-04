using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverShiba : MonoBehaviour
{
    private string crtShiba;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        crtShiba = PlayerPrefs.GetString("Shiba", "isIdle");
        ChangeSkin();
    }

    private void ChangeSkin()
    {
        switch (crtShiba)
        {
            case "isIdle":
                animator.Play("Idle_Shiba");
                break;

            case "isStrawberry":
                animator.Play("Strawberry_Shiba");
                break;

            case "isMint":
                animator.Play("Mint_Shiba");
                break;

            case "isDevil":
                animator.Play("Devil_Shiba");
                break;

            case "isAngel":
                animator.Play("Angel_Shiba");
                break;

            case "isMelona":
                animator.Play("Melona_Shiba");
                break;
        }
    }
}
