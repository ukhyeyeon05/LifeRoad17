using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroNarration : MonoBehaviour
{
    [Header("UI 오브젝트들")]
    public GameObject logoImage;
    public GameObject startButton;
    public GameObject puzzleImage;
    public GameObject fullscreenPanel;
    public TextMeshProUGUI narrationText;
    public GameObject clickGuide;
    public GameObject puzzlePiece_1;

    [Header("표시할 문장들")]
    [TextArea(2, 5)]
    public string[] narrationLines;

    private int currentLine = 0;
    private bool isTyping = false;

    public static bool isPuzzleUnlocked = false;

    private CanvasGroup puzzlePieceGroup;

    void Start()
    {
        logoImage.SetActive(true);
        startButton.SetActive(true);

        puzzleImage.SetActive(false);
        fullscreenPanel.SetActive(false);
        clickGuide.SetActive(false);
        narrationText.text = "";

        isPuzzleUnlocked = false;

        if (puzzlePiece_1 != null)
        {
            puzzlePiece_1.SetActive(false);
            puzzlePieceGroup = puzzlePiece_1.GetComponent<CanvasGroup>();
            if (puzzlePieceGroup != null)
                puzzlePieceGroup.alpha = 0f;
        }
    }

    public void OnStartButtonClick()
    {
        Debug.Log("Start 버튼 클릭됨!");

        logoImage.SetActive(false);
        startButton.SetActive(false);
        puzzleImage.SetActive(true);
        fullscreenPanel.SetActive(true);
        ShowNextLine();
    }

    public void OnScreenClick()
    {
        if (isTyping) return;

        clickGuide.SetActive(false);

        if (currentLine < narrationLines.Length)
        {
            ShowNextLine();
        }
        else
        {
            Debug.Log("문장 끝. 퍼즐 선택 가능!");
            isPuzzleUnlocked = true;

            if (puzzlePiece_1 != null)
            {
                puzzlePiece_1.SetActive(true);
                if (puzzlePieceGroup != null)
                    StartCoroutine(FadeInPuzzlePiece(puzzlePieceGroup, 1.5f)); // 1.5초 동안 서서히
            }
        }
    }

    void ShowNextLine()
    {
        StartCoroutine(TypeText(narrationLines[currentLine]));
        currentLine++;
    }

    IEnumerator TypeText(string line)
    {
        isTyping = true;
        narrationText.text = "";

        foreach (char c in line)
        {
            narrationText.text += c;
            yield return new WaitForSeconds(0.03f); // 타자 효과
        }

        isTyping = false;
        clickGuide.SetActive(true);
    }

    IEnumerator FadeInPuzzlePiece(CanvasGroup group, float duration)
    {
        float elapsed = 0f;
        group.alpha = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            group.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }

        group.alpha = 1f;
    }
}
