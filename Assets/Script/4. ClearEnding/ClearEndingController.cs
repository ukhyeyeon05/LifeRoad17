using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ClearEndingController : MonoBehaviour
{
    [Header("UI 텍스트")]
    public TextMeshProUGUI typingText;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI factText;

    [Header("버튼과 클릭 가이드")]
    public Button nextPuzzleButton;
    public GameObject clickGuideImage;
    public GameObject fullscreenPanel;

    [Header("CanvasGroup")]
    public CanvasGroup typingGroup;
    public CanvasGroup feedbackGroup;
    public CanvasGroup factGroup;
    public CanvasGroup buttonGroup;

    [TextArea]
    public string typingSentence = "당신은 이번 인생의 Key를 끝까지 지켜냈습니다.";
    [TextArea]
    public string factSentence = "실제 고령화는 전세계적으로 빠르게 진행 중이지만,\n노후 복지와 연금 개혁은 그 속도를 따라오지 못하고 있습니다.\n또한 수많은 빈곤층들이 사회 보장 시스템의 사각지대에 놓여 있습니다.\n\n- UN(2023), OECD(2023), World Bank Group(2025) -";

    private int currentPhase = 0;
    private bool canClick = false;

    void Start()
    {
        // 초기 상태 설정
        feedbackGroup.alpha = 0;
        factGroup.alpha = 0;
        buttonGroup.alpha = 0;
        typingGroup.alpha = 1;

        typingText.text = "";
        feedbackText.text = "";
        factText.text = "";

        nextPuzzleButton.interactable = false;
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

    public void NextStep() // FullscreenPanel에 연결
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
                StartCoroutine(ShowButton());
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

    IEnumerator ShowButton()
    {
        yield return FadeCanvasGroup(factGroup, false);

        // FullscreenPanel 비활성화해서 버튼 클릭 가능
        if (fullscreenPanel != null)
            fullscreenPanel.SetActive(false);

        yield return FadeCanvasGroup(buttonGroup, true);
        nextPuzzleButton.interactable = true;
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
        group.gameObject.SetActive(fadeIn); // 안 보일 땐 비활성화
    }
}
