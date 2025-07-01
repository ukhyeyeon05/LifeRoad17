using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToChapterSelect : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("ChapterSelect");
    }
}
