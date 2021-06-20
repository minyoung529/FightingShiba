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

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Mainbgm;
        audioSource.Play();
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
}
