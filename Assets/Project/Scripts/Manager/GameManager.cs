using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool hasSavedData = false;
    public string lastPuzzleId = "";
    public string lastPuzzleSceneName = "";
    public EndingResult lastResult;

    public float bgmVolume = 0.5f;
    public float sfxVolume = 0.5f;
    public int languageIndex = 0;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//씬 유지
            LoadOptionSettings(); // 옵션 설정 불러오기
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll(); // 저장 초기화
        hasSavedData = false;
        SceneManager.LoadScene("NarrationScene");
    }

    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey("SavedPuzzle")) return;
        lastPuzzleId = PlayerPrefs.GetString("SavedPuzzle");
        hasSavedData = true;
        SceneManager.LoadScene("ChapterSelect");
    }

    public void SaveGame(string puzzleId)
    {
        PlayerPrefs.SetString("SavedPuzzle", puzzleId);
    }

    public void LoadOptionSettings() //옵션 설정 값 불러오기
    {
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        languageIndex = PlayerPrefs.GetInt("LanguageIndex", 0);

        Debug.Log($"옵션 불러오기 - BGM: {bgmVolume}, SFX: {sfxVolume}, Lang: {languageIndex}");

       
    }
}

public enum EndingResult
{
    Normal,
    Bad
}
