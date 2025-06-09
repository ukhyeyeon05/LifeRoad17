using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleLoader : MonoBehaviour
{
    public string sceneName;

    public void LoadPuzzle()
    {
        SceneManager.LoadScene(sceneName);
    }
}
