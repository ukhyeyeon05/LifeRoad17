using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class ChapterSelectManager : MonoBehaviour
{
    [Header("����")]
    public ChapterMeta chapterMeta;                     // ���� ������ ����
    public List<Button> chapterButtons;                 // UI�� ��ġ�� 17���� ��ư (Inspector���� ������� �Ҵ�)

    void Start()
    {
        for (int i = 0; i < chapterMeta.chapters.Count; i++)
        {
            int index = i; // Ŭ���� ���� ����

            var chapter = chapterMeta.chapters[index];
     
            var button = chapterButtons[index];

            // ��ư �ؽ�Ʈ ���� (��: "Puz01")
            var text = button.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null) text.text = chapter.puzzleId;

            // �̹� Ŭ����� ������ ��Ȱ��ȭ
            if (chapter.status == PuzzleStatus.Cleared_Normal)
            {
                button.interactable = false;
                var img = button.GetComponent<Image>();
                if (img != null) img.color = Color.gray;
            }
            else
            {
                button.onClick.AddListener(() => OnPuzzleClicked(chapter.sceneName));
            }
        }

        // ��� ������ Cleared_Normal���� üũ
        if (AllChaptersCleared())
        {
            SceneManager.LoadScene("AllClearScene");
        }
    }

    void OnPuzzleClicked(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    bool AllChaptersCleared()
    {
        foreach (var chapter in chapterMeta.chapters)
        {
            if (chapter.status != PuzzleStatus.Cleared_Normal)
                return false;
        }
        return true;
    }
}
