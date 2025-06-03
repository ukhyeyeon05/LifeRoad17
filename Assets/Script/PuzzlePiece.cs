using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzlePiece_1 : MonoBehaviour
{
    public void OnPuzzlePieceClicked()
    {
        // ������ �� ���� �Ŀ��� Ŭ�� ����
        if (!IntroNarration.isPuzzleUnlocked)
        {
            Debug.Log("���� ������ �� �����ϴ�. ������ ������ �о��ּ���.");
            return;
        }

        Debug.Log("���� 1 Ŭ����! �� �� ��ȯ: Puz1_Starting");
        SceneManager.LoadScene("Puz1_Starting");
    }
}
