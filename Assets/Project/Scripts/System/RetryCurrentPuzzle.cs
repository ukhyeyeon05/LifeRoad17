using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryCurrentPuzzle : MonoBehaviour
{
    private string puzzleSceneName;

    void Start()
    {
        puzzleSceneName = PlayerPrefs.GetString("LastPlayedPuzzleScene", "");
    }

    public void OnClick()
    {
        if (!string.IsNullOrEmpty(puzzleSceneName))
        {
            PlayerStatus.Instance.ResetStatus();
            SceneManager.LoadScene(puzzleSceneName);
        }
        else
        {
            Debug.LogWarning("Retry 실패: 퍼즐 씬 이름이 저장되지 않았습니다.");
        }
    }
}
