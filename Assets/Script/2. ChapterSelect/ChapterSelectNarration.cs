using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChapterSelectNarration : MonoBehaviour
{
    public TextMeshProUGUI narrationText;
    public GameObject clickGuide;
    public GameObject fullscreenPanel;
    public Button[] puzzleButtons;

    [TextArea(2, 5)]
    public string[] introLines;
    public string afterStoryMessage = "퍼즐을 선택해주세요.";

    private int currentLine = 0;
    private bool isTyping = false;
    private bool isIntro = true;

    void Start()
    {
        // 분기점 이후 돌아온 경우 여부 체크 (GameManager로 대체 가능)
        isIntro = PlayerPrefs.GetInt("isIntroDone", 0) == 0;

        clickGuide.SetActive(false);
        fullscreenPanel.SetActive(isIntro);

        SetButtonsInteractable(false);
        narrationText.text = "";

        if (isIntro)
        {
            ShowNextLine();
        }
        else
        {
            narrationText.text = afterStoryMessage;
            SetButtonsInteractable(true);
        }
    }

    public void OnScreenClick()
    {
        if (isTyping || !isIntro) return;

        clickGuide.SetActive(false);

        if (currentLine < introLines.Length)
        {
            ShowNextLine();
        }
        else
        {
            // 나레이션 끝나고 버튼 활성화
            PlayerPrefs.SetInt("isIntroDone", 1);
            PlayerPrefs.Save();

            SetButtonsInteractable(true);
            fullscreenPanel.SetActive(false);
            narrationText.text = afterStoryMessage;
        }
    }

    void ShowNextLine()
    {
        StartCoroutine(TypeText(introLines[currentLine]));
        currentLine++;
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;
        narrationText.text = "";

        foreach (char c in line)
        {
            narrationText.text += c;
            yield return new WaitForSeconds(0.03f);
        }

        isTyping = false;
        clickGuide.SetActive(true);
    }

    void SetButtonsInteractable(bool value)
    {
        foreach (Button btn in puzzleButtons)
            btn.interactable = value;
    }
}
