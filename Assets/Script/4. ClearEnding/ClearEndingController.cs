using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ClearEndingController : MonoBehaviour
{
    [Header("UI �ؽ�Ʈ")]
    public TextMeshProUGUI typingText;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI factText;

    [Header("��ư�� Ŭ�� ���̵�")]
    public Button nextPuzzleButton;
    public GameObject clickGuideImage;
    public GameObject fullscreenPanel;

    [Header("CanvasGroup")]
    public CanvasGroup typingGroup;
    public CanvasGroup feedbackGroup;
    public CanvasGroup factGroup;
    public CanvasGroup buttonGroup;

    [TextArea]
    public string typingSentence = "����� �̹� �λ��� Key�� ������ ���ѳ½��ϴ�.";
    [TextArea]
    public string factSentence = "���� ���ȭ�� ������������ ������ ���� ��������,\n���� ������ ���� ������ �� �ӵ��� ������� ���ϰ� �ֽ��ϴ�.\n���� ������ ��������� ��ȸ ���� �ý����� �簢���뿡 ���� �ֽ��ϴ�.\n\n- UN(2023), OECD(2023), World Bank Group(2025) -";

    private int currentPhase = 0;
    private bool canClick = false;

    void Start()
    {
        // �ʱ� ���� ����
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

    public void NextStep() // FullscreenPanel�� ����
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
        feedbackText.text = "��̳���?";
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

        // FullscreenPanel ��Ȱ��ȭ�ؼ� ��ư Ŭ�� ����
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
        group.gameObject.SetActive(fadeIn); // �� ���� �� ��Ȱ��ȭ
    }
}
