using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChapterButton : MonoBehaviour
{
    public string puzzleId;           // 예: "Puz1"
    public string puzzleSceneName;    // 예: "Puz1_Starting"

    void Start()
    {
        Button button = GetComponent<Button>();
        if (button == null || GameManager.Instance == null) return;

        string normalizedId = puzzleId.Trim(); // 공백 제거

        bool isCleared = GameManager.Instance.IsPuzzleCleared(normalizedId);
        Debug.Log($"[ChapterButton] puzzleId = {normalizedId}, Cleared? = {isCleared}");

        if (isCleared)
        {
            button.interactable = false;
        }
        else
        {
            button.onClick.AddListener(() =>
            {
                GameManager.Instance.lastPuzzleId = normalizedId;
                GameManager.Instance.lastPuzzleSceneName = puzzleSceneName;
                GameManager.Instance.SaveGame(normalizedId);
                SceneManager.LoadScene(puzzleSceneName);
            });
        }
    }
}
