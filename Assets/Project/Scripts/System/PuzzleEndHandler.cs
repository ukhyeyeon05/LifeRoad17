using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleEndHandler : MonoBehaviour
{
    public void GoToEndingScene(string endingType)
    {
        // 현재 퍼즐 씬 이름 저장
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastPlayedPuzzleScene", currentScene);

        // Normal 엔딩일 경우 클리어 여부 저장
        if (endingType == "Normal")
        {
            PlayerPrefs.SetInt("Cleared_" + currentScene, 1); // 예: "Cleared_Puz1_Starting"
        }

        // 엔딩 씬으로 전환
        SceneManager.LoadScene("EndingScene");
    }
}
