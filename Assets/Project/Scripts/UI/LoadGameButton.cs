using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameButton : MonoBehaviour
{
    public void OnClickLoadGame()
    {
        if (!SaveSystem.HasSavedGame())
        {
            Debug.Log("저장된 게임이 없습니다.");
            return;
        }

        SaveData data = SaveSystem.LoadGame();
        if (data != null)
        {
            string sceneName = data.puzzleId + "_Starting";
            GameManager.Instance.lastPuzzleSceneName = sceneName;
            SceneManager.LoadScene(sceneName);
        }
    }
}
