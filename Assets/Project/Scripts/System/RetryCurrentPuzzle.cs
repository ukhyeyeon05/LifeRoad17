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
            Debug.LogWarning("Retry ����: ���� �� �̸��� ������� �ʾҽ��ϴ�.");
        }
    }
}
