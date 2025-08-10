using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryCurrentPuzzle : MonoBehaviour
{
    private string puzzleSceneName;

    void Start()
    {
        // 저장된 씬 이름 불러오기
        puzzleSceneName = PlayerPrefs.GetString("LastPlayedPuzzleScene", "");
    }

    public void OnClick()
    {
        if (!string.IsNullOrEmpty(puzzleSceneName))
        {
            // 상태 초기화 (있다면)
            if (PlayerStatus.Instance != null)
            {
                PlayerStatus.Instance.ResetStatus();
            }

            Debug.Log($"[Retry] 다시 로드: {puzzleSceneName}");
            SceneManager.LoadScene(puzzleSceneName);
        }
        else
        {
            Debug.LogWarning("Retry 실패: 퍼즐 씬 이름이 저장되지 않았습니다.");
        }
    }
}
