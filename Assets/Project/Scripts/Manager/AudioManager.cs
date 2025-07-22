using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void SetBGMVolume(float v)
    {
        if (bgmSource != null)
            bgmSource.volume = v;
    }

    public void SetSFXVolume(float v)
    {
        if (sfxSource != null)
            sfxSource.volume = v;
    }
}
