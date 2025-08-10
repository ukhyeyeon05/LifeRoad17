using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndingManager : MonoBehaviour
{
    public TextMeshProUGUI endingText;
    public GameObject buttonPanelNormal;
    public GameObject buttonPanelBad;
    public GameObject arrowGuide;
    public EndingData[] endingDataList;

    private string puzzleId;
    private string endingType;
    private int money, health;

    private string[] textSteps = new string[0]; // ← null 방지용 기본값
    private int currentStep = 0;

    private CanvasGroup textGroup;
    private bool isTransitioning = false;

    void Start()
    {
        puzzleId = PlayerPrefs.GetString("puzzleId", "");
        endingType = PlayerPrefs.GetString("endingType", "Normal");
        money = PlayerPrefs.GetInt("money", 0);
        health = PlayerPrefs.GetInt("health", 0);

        Debug.Log($"[EndingManager] 불러온 puzzleId = {puzzleId}");

        if (buttonPanelNormal != null) buttonPanelNormal.SetActive(false);
        if (buttonPanelBad != null) buttonPanelBad.SetActive(false);
        if (arrowGuide != null) arrowGuide.SetActive(false);

        textGroup = endingText.GetComponent<CanvasGroup>();
        if (textGroup == null)
            textGroup = endingText.gameObject.AddComponent<CanvasGroup>();

        EndingData data = GetEndingData();
        if (data == null)
        {
            endingText.text = "해당 퍼즐의 엔딩 정보를 찾을 수 없습니다.";
            textSteps = new string[0]; // ← null 방지
            return;
        }

        textSteps = new string[]
        {
            endingType == "Normal" ? data.normalTitle : data.badTitle,
            endingType == "Normal" ? data.normalFeedback : data.badFeedback,
            "어떠셨나요?",
            data.realWorldInfo
        };

        currentStep = 0;
        StartCoroutine(ShowTextStep(textSteps[currentStep]));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTransitioning && currentStep < textSteps.Length)
        {
            if (arrowGuide != null)
                arrowGuide.SetActive(false);

            currentStep++;
            if (currentStep < textSteps.Length)
            {
                StartCoroutine(ShowTextStep(textSteps[currentStep]));
            }
            else
            {
                StartCoroutine(FadeInButton());
            }
        }
    }

    EndingData GetEndingData()
    {
        foreach (var data in endingDataList)
        {
            if (data != null && data.puzzleId == puzzleId)
                return data;
        }

        Debug.LogWarning($"[EndingManager] puzzleId에 맞는 EndingData를 찾을 수 없습니다: {puzzleId}");
        return null;
    }

    IEnumerator ShowTextStep(string message)
    {
        isTransitioning = true;

        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            textGroup.alpha = 1f - t;
            yield return null;
        }

        textGroup.alpha = 0f;
        endingText.text = message;

        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            textGroup.alpha = t;
            yield return null;
        }

        textGroup.alpha = 1f;

        if (arrowGuide != null)
        {
            arrowGuide.SetActive(true);
            StartCoroutine(BlinkArrow());
        }

        isTransitioning = false;
    }

    IEnumerator BlinkArrow()
    {
        while (arrowGuide.activeSelf && currentStep < textSteps.Length)
        {
            arrowGuide.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            arrowGuide.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FadeInButton()
    {
        GameObject panelToShow = endingType == "Normal" ? buttonPanelNormal : buttonPanelBad;

        // 퍼즐 클리어 저장
        if (endingType == "Normal" && !string.IsNullOrEmpty(puzzleId))
        {
            GameManager.Instance.MarkPuzzleAsCleared(puzzleId.Trim());
            Debug.Log($"[EndingManager] 클리어 저장됨: Clear_{puzzleId}");
        }

        if (panelToShow != null)
        {
            panelToShow.SetActive(true);
            CanvasGroup group = panelToShow.GetComponent<CanvasGroup>();
            if (group == null) group = panelToShow.AddComponent<CanvasGroup>();

            group.alpha = 0f;
            for (float t = 0; t < 1f; t += Time.deltaTime)
            {
                group.alpha = t;
                yield return null;
            }
            group.alpha = 1f;
        }
    }
}
