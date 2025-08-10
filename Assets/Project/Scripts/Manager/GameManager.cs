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
            DontDestroyOnLoad(gameObject);
            LoadOptionSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NewGame()
    {
        Debug.Log("[GameManager] 새 게임 시작 - 모든 저장 초기화 (퍼즐 클리어 기록 포함)");

        PlayerPrefs.DeleteAll(); //퍼즐 클리어 포함 전체 삭제
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

    public void LoadOptionSettings()
    {
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        languageIndex = PlayerPrefs.GetInt("LanguageIndex", 0);
    }

    public bool IsPuzzleCleared(string puzzleId)
    {
        string key = "Clear_" + puzzleId.Trim(); // 항상 일치된 형식
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    public void MarkPuzzleAsCleared(string puzzleId)
    {
        string key = "Clear_" + puzzleId.Trim();
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
        Debug.Log($"[GameManager] 퍼즐 클리어 저장됨: {key}");
    }
}

public enum EndingResult
{
    Normal,
    Bad
}
