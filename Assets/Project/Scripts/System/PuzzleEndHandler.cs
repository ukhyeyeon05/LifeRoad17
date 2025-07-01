using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleEndHandler : MonoBehaviour
{
    public void GoToEndingScene(string endingType)
    {
        // ���� ���� �� �̸� ����
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastPlayedPuzzleScene", currentScene);

        // Normal ������ ��� Ŭ���� ���� ����
        if (endingType == "Normal")
        {
            PlayerPrefs.SetInt("Cleared_" + currentScene, 1); // ��: "Cleared_Puz1_Starting"
        }

        // ���� ������ ��ȯ
        SceneManager.LoadScene("EndingScene");
    }
}
