using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioClip clickSFX;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = clickSFX;
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSFX);
    }
}
