using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSoundManager : MonoBehaviour
{
    public static ShopSoundManager Instance;

    AudioSource audioSource;
    [SerializeField]
    AudioClip buttonSound;
    [SerializeField]
    AudioClip paySound;
    [SerializeField]
    AudioClip errorSound;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }

    public void PaySound()
    {
        audioSource.PlayOneShot(paySound);
    }

    public void ErrorSound()
    {
        audioSource.PlayOneShot(errorSound);
    }
}
