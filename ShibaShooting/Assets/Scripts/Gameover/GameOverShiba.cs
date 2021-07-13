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
                animator.Play("Shop_Idle");
                break;

            case "isStrawberry":
                animator.Play("Shop_Strawberry_Shiba");
                break;

            case "isMint":
                animator.Play("Shop_Mint_Shiba");
                break;

            case "isDevil":
                animator.Play("Shop_Devil");
                break;

            case "isAngel":
                animator.Play("Shop_Angel");
                break;

            case "isMelona":
                animator.Play("Shop_Melona");
                break;
        }
    }
}
