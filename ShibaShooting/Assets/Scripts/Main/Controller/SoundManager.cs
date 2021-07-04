using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip Mainbgm;
    [SerializeField]
    private AudioClip coinSound;
    [SerializeField]
    private AudioClip itemSound;
    [SerializeField]
    private AudioClip lightningSound;
    [SerializeField]
    private AudioClip damagedSound;
    [SerializeField]
    private AudioClip buttonSound;
    [SerializeField]
    private AudioClip tutorialBGM;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if(PlayerPrefs.GetString("First") != "true")
        {
            audioSource.clip = Mainbgm;
            audioSource.Play();
        }

        if (PlayerPrefs.GetString("First", "true") == "true")
        {
            Tutorial();
        }
    }

    public void CoinAudio()
    {
        audioSource.PlayOneShot(coinSound);
    }

    public void ItemAudio()
    {
        audioSource.PlayOneShot(itemSound);
    }

    public void LightningAudio()
    {
        audioSource.PlayOneShot(lightningSound);
    }

    public void DamagedAudio()
    {
        audioSource.PlayOneShot(damagedSound);
    }

    public void ButtonAudio()
    {
        audioSource.PlayOneShot(buttonSound);
    }

    public void Slow()
    {
        audioSource.pitch = 0.7f;
    }

    public void DefaultSpeed()
    {
        audioSource.pitch = 1f;
    }

    public void Tutorial()
    {
        audioSource.clip = tutorialBGM;
        audioSource.Play();
    }

    public void EndTutorial()
    {
        audioSource.clip = Mainbgm;
        audioSource.Play();
    }
}
