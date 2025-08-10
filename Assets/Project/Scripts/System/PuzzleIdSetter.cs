using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleIdSetter : MonoBehaviour
{
    public string puzzleId = "Puz1"; // 반드시 정확히 입력 (대소문자 포함)

    private void Awake()
    {
        string normalizedId = puzzleId.Trim();
        PlayerPrefs.SetString("puzzleId", normalizedId);

        string currentSceneName = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastPlayedPuzzleScene", currentSceneName);

        Debug.Log($"[PuzzleIdSetter] puzzleId 저장됨: {normalizedId}, 씬 이름 저장됨: {currentSceneName}");
    }
}
