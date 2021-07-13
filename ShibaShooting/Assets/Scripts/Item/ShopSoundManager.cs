using UnityEngine;

public class ShopSoundManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    AudioClip buttonSound;
    [SerializeField]
    AudioClip paySound;
    [SerializeField]
    AudioClip errorSound;
    [SerializeField]
    AudioClip fieldSound;

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

    public void FieldSound()
    {
        audioSource.PlayOneShot(fieldSound);
    }

    public void ErrorSound()
    {
        audioSource.PlayOneShot(errorSound);
    }
}
