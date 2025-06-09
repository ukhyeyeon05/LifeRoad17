using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public Text resultText;
    public Button homeButton;

    void Start()
    {
        bool isHappyEnding = CalculateEnding();

        resultText.text = isHappyEnding ? "해피 엔딩" : "배드 엔딩";

        int puzzleID = PlayerPrefs.GetInt("선택된 주제", 0);
        PlayerPrefs.SetInt($"SDGs No.{puzzleID} 완료", 1);
        PlayerPrefs.Save();

        homeButton.onClick.AddListener(() => SceneManager.LoadScene("0_ChapterSelect"));
    }

    bool CalculateEnding()
    {
        int endingScore = 0;
        int totalBranches = 10;

        for (int branch = 1; branch <= totalBranches; branch++)
        {
            int choice = PlayerPrefs.GetInt($"선택_{branch}", -1);

            if (choice == -1)
                return false;

            if (choice == 1 || choice == 2)
                endingScore++;
        }
        return endingScore >= 7;
    }
}