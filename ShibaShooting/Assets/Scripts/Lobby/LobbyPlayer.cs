using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayer : MonoBehaviour
{
    Animator animator;
    private string crtShiba;
    void Start()
    {
        animator = GetComponent<Animator>();
        crtShiba = PlayerPrefs.GetString("Shiba", "isIdle");

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
