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

        resultText.text = isHappyEnding ? "���� ����" : "��� ����";

        int puzzleID = PlayerPrefs.GetInt("���õ� ����", 0);
        PlayerPrefs.SetInt($"SDGs No.{puzzleID} �Ϸ�", 1);
        PlayerPrefs.Save();

        homeButton.onClick.AddListener(() => SceneManager.LoadScene("0_ChapterSelect"));
    }

    bool CalculateEnding()
    {
        int endingScore = 0;
        int totalBranches = 10;

        for (int branch = 1; branch <= totalBranches; branch++)
        {
            int choice = PlayerPrefs.GetInt($"����_{branch}", -1);

            if (choice == -1)
                return false;

            if (choice == 1 || choice == 2)
                endingScore++;
        }
        return endingScore >= 7;
    }
}