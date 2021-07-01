using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    private string crtShiba;

    void Start()
    {
        crtShiba = PlayerPrefs.GetString("Shiba", "isIdle");
    }

    public void OnClickIdle()
    {
        crtShiba = "isIdle";
        PlayerPrefs.SetString("Shiba", crtShiba);
    }

    public void OnClickStrawberry()
    {
        crtShiba = "isStrawberry";
        PlayerPrefs.SetString("Shiba", crtShiba);
    }
}
