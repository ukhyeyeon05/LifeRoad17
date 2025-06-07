using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzlePiece_1 : MonoBehaviour
{
    public void OnPuzzlePieceClicked()
    {
        // 문장이 다 끝난 후에만 클릭 가능
        if (!IntroNarration.isPuzzleUnlocked)
        {
            Debug.Log("아직 선택할 수 없습니다. 문장을 끝까지 읽어주세요.");
            return;
        }

        Debug.Log("퍼즐 1 클릭됨! → 씬 전환: Puz1_Starting");
        SceneManager.LoadScene("Puz1_Starting");
    }
}
