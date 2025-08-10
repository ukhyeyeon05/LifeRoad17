using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    public void OnClickExit()
    {
        Debug.Log("[ExitGameButton] 게임 종료 요청");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
