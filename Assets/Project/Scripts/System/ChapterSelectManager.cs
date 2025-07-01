using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class ChapterSelectManager : MonoBehaviour
{
    [Header("참조")]
    public ChapterMeta chapterMeta;                     // 퍼즐 데이터 모음
    public List<Button> chapterButtons;                 // UI에 배치된 17개의 버튼 (Inspector에서 순서대로 할당)

    void Start()
    {
        for (int i = 0; i < chapterMeta.chapters.Count; i++)
        {
            int index = i; // 클로저 문제 방지

            var chapter = chapterMeta.chapters[index];
     
            var button = chapterButtons[index];

            // 버튼 텍스트 설정 (예: "Puz01")
            var text = button.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null) text.text = chapter.puzzleId;

            // 이미 클리어된 퍼즐은 비활성화
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

        // 모든 퍼즐이 Cleared_Normal인지 체크
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
