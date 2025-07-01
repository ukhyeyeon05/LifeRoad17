using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool hasSavedData = false;
    public string lastPuzzleId = "";
    public string lastPuzzleSceneName = "";
    public EndingResult lastResult;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//���� �ٲ� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll(); // ���� �ʱ�ȭ
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
}

public enum EndingResult
{
    Normal,
    Bad
}
