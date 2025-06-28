using UnityEngine;
using UnityEngine.SceneManagement;

public class BedEndingManager : MonoBehaviour
{
    public void RetryPuzzle()
    {
        string lastScene = PlayerPrefs.GetString("LastPuzzleScene", "Puz1_Starting");
        GameManager.Instance.ResetStatusAndLoadScene(lastScene);
    }

    public void GoToPuzzleSelect()
    {
        SceneManager.LoadScene("0_ChapterSelect");
    }
}
