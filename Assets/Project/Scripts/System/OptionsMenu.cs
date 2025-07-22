using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    [Header("UI")]
    public Slider bgmSlider;
    public Slider sfxSlider;
    public TMP_Dropdown languageDropdown;

    void Start()
    {
        // PlayerPrefs에서 기존 값 불러오기
        bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        languageDropdown.value = PlayerPrefs.GetInt("LanguageIndex", 0);

        // 이벤트 연결
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        languageDropdown.onValueChanged.AddListener(SetLanguage);
    }

    void SetBGMVolume(float volume)
    {
        PlayerPrefs.SetFloat("BGMVolume", volume);
        // AudioManager.Instance?.SetBGMVolume(volume); ← 연결 가능
    }

    void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume", volume);
        // AudioManager.Instance?.SetSFXVolume(volume);
    }

    void SetLanguage(int index)
    {
        PlayerPrefs.SetInt("LanguageIndex", index);
        // LanguageManager.Instance?.SetLanguage(index);
    }

    public void ReturnToIntro()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
