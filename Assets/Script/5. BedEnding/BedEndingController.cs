using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BedEndingController : MonoBehaviour
{
    [Header("UI 텍스트")]
    public TextMeshProUGUI typingText;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI factText;

    [Header("버튼과 클릭 가이드")]
    public GameObject clickGuideImage;
    public GameObject fullscreenPanel;

    [Header("CanvasGroup")]
    public CanvasGroup typingGroup;
    public CanvasGroup feedbackGroup;
    public CanvasGroup factGroup;
    public CanvasGroup buttonGroup;

    [Header("베드엔딩 버튼")]
    public Button retryButton;
    public Button goSelectButton;

    [TextArea]
    public string typingSentence = "당신은 이번 인생의 Key를 무사히 지키지 못했습니다.";
    [TextArea]
    public string factSentence = "빈곤 문제는 단지 개인의 문제가 아닙니다.\n지속가능한 사회안전망 구축 없이는, 많은 이들이 위기에 놓일 수 있습니다.";

    private int currentPhase = 0;
    private bool canClick = false;

    void Start()
    {
        feedbackGroup.alpha = 0;
        factGroup.alpha = 0;
        buttonGroup.alpha = 0;
        typingGroup.alpha = 1;

        typingText.text = "";
        feedbackText.text = "";
        factText.text = "";

        retryButton.interactable = false;
        goSelectButton.interactable = false;
        clickGuideImage.SetActive(false);

        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char c in typingSentence)
        {
            typingText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.5f);
        clickGuideImage.SetActive(true);
        canClick = true;
    }

    public void NextStep()
    {
        if (!canClick) return;

        canClick = false;
        clickGuideImage.SetActive(false);
        currentPhase++;

        switch (currentPhase)
        {
            case 1:
                StartCoroutine(SwitchTypingToFeedback());
                break;
            case 2:
                StartCoroutine(SwitchFeedbackToFact());
                break;
            case 3:
                StartCoroutine(ShowButtons());
                break;
        }
    }

    IEnumerator SwitchTypingToFeedback()
    {
        yield return FadeCanvasGroup(typingGroup, false);
        feedbackText.text = "어떠셨나요?";
        yield return FadeCanvasGroup(feedbackGroup, true);
        yield return new WaitForSeconds(0.3f);
        clickGuideImage.SetActive(true);
        canClick = true;
    }

    IEnumerator SwitchFeedbackToFact()
    {
        yield return FadeCanvasGroup(feedbackGroup, false);
        factText.text = factSentence;
        yield return FadeCanvasGroup(factGroup, true);
        yield return new WaitForSeconds(0.3f);
        clickGuideImage.SetActive(true);
        canClick = true;
    }

    IEnumerator ShowButtons()
    {
        yield return FadeCanvasGroup(factGroup, false);

        // 여기에서 Raycast를 꺼줘야 버튼이 클릭 가능
        if (fullscreenPanel != null)
        {
            CanvasGroup cg = fullscreenPanel.GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cg.interactable = false;
                cg.blocksRaycasts = false;
            }
        }

        yield return FadeCanvasGroup(buttonGroup, true);
        retryButton.interactable = true;
        goSelectButton.interactable = true;
    }



    IEnumerator FadeCanvasGroup(CanvasGroup group, bool fadeIn, float duration = 0.8f)
    {
        float time = 0f;
        float start = fadeIn ? 0 : 1;
        float end = fadeIn ? 1 : 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            group.alpha = Mathf.Lerp(start, end, time / duration);
            yield return null;
        }

        group.alpha = end;
        group.gameObject.SetActive(fadeIn);
    }

    // 버튼 기능
    public void RetryPuzzle()
    {
        string lastScene = PlayerPrefs.GetString("LastPuzzleScene", "Puz1_Starting");
        UnityEngine.SceneManagement.SceneManager.LoadScene(lastScene);
    }

    public void GoToPuzzleSelect()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("0_ChapterSelect");
    }
}
