using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearEnding : MonoBehaviour
{
    public void OnClickReturn()
    {
        SceneManager.LoadScene("0_ChapterSelect");
    }
}
