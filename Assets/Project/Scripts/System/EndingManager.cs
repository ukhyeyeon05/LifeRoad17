/*using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndingManager : MonoBehaviour
{
    public TextMeshProUGUI endingText;
    public GameObject buttonPanelNormal;
    public GameObject buttonPanelBad;
    public EndingData[] endingDataList;
    public AudioSource clickSound;

    private string puzzleId;
    private string endingType;
    private int money, health;

    private string[] textSteps;
    private int currentStep = 0;

    private CanvasGroup textGroup;
    private bool isTransitioning = false;

    void Start()
    {
        // ���� ���� �ε�
        puzzleId = PlayerPrefs.GetString("puzzleId", "Puz01");
        endingType = PlayerPrefs.GetString("endingType", "Normal");
        money = PlayerPrefs.GetInt("money", 0);
        health = PlayerPrefs.GetInt("health", 0);

        // �г� ��� ����
        buttonPanelNormal.SetActive(false);
        buttonPanelBad.SetActive(false);

        // �ؽ�Ʈ �׷� ����
        textGroup = endingText.GetComponent<CanvasGroup>();
        if (textGroup == null) textGroup = endingText.gameObject.AddComponent<CanvasGroup>();

        // ���� ������ �ҷ�����
        EndingData data = GetEndingData();
        if (data == null)
        {
            endingText.text = "�ش� ������ ���� ������ ã�� �� �����ϴ�.";
            return;
        }

        // ����� �ؽ�Ʈ �ܰ� ����
        textSteps = new string[]
        {
            endingType == "Normal" ? data.normalTitle : data.badTitle,
            endingType == "Normal" ? data.normalFeedback : data.badFeedback,
            "��̳���?",
            data.realWorldInfo
        };

        currentStep = 0;
        StartCoroutine(ShowTextStep(textSteps[currentStep]));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTransitioning && currentStep < textSteps.Length)
        {
            clickSound?.Play();

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
            if (data.puzzleId == puzzleId)
                return data;
        }
        return null;
    }

    IEnumerator ShowTextStep(string message)
    {
        isTransitioning = true;

        // ���̵� �ƿ�
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            textGroup.alpha = 1f - t;
            yield return null;
        }

        textGroup.alpha = 0f;
        endingText.text = message;

        // ���̵� ��
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            textGroup.alpha = t;
            yield return null;
        }

        textGroup.alpha = 1f;
        isTransitioning = false;
    }

    IEnumerator FadeInButton()
    {
        GameObject panelToShow = endingType == "Normal" ? buttonPanelNormal : buttonPanelBad;

        CanvasGroup group = panelToShow.GetComponent<CanvasGroup>();
        if (group == null) group = panelToShow.AddComponent<CanvasGroup>();

        panelToShow.SetActive(true);
        group.alpha = 0f;

        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            group.alpha = t;
            yield return null;
        }

        group.alpha = 1f;
    }
}
*/
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndingManager : MonoBehaviour
{
    public TextMeshProUGUI endingText;
    public GameObject buttonPanelNormal;
    public GameObject buttonPanelBad;
    public GameObject arrowGuide; // �����̴� ȭ��ǥ
    public EndingData[] endingDataList;
    public AudioSource clickSound;

    private string puzzleId;
    private string endingType; // "Normal" or "Bad"
    private int money, health;

    private string[] textSteps;
    private int currentStep = 0;

    private CanvasGroup textGroup;
    private bool isTransitioning = false;

    void Start()
    {
        puzzleId = PlayerPrefs.GetString("puzzleId", "Puz01");
        endingType = PlayerPrefs.GetString("endingType", "Normal");
        money = PlayerPrefs.GetInt("money", 0);
        health = PlayerPrefs.GetInt("health", 0);

        if (buttonPanelNormal != null) buttonPanelNormal.SetActive(false);
        if (buttonPanelBad != null) buttonPanelBad.SetActive(false);
        if (arrowGuide != null) arrowGuide.SetActive(false);

        textGroup = endingText.GetComponent<CanvasGroup>();
        if (textGroup == null)
        {
            textGroup = endingText.gameObject.AddComponent<CanvasGroup>();
        }

        EndingData data = GetEndingData();
        if (data == null)
        {
            endingText.text = "�ش� ������ ���� ������ ã�� �� �����ϴ�.";
            return;
        }

        textSteps = new string[]
        {
            endingType == "Normal" ? data.normalTitle : data.badTitle,
            endingType == "Normal" ? data.normalFeedback : data.badFeedback,
            "��̳���?",
            data.realWorldInfo
        };

        currentStep = 0;
        StartCoroutine(ShowTextStep(textSteps[currentStep]));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTransitioning && currentStep < textSteps.Length)
        {
            if (arrowGuide != null) arrowGuide.SetActive(false);
            clickSound?.Play();

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
            if (data.puzzleId == puzzleId)
                return data;
        }
        return null;
    }

    IEnumerator ShowTextStep(string message)
    {
        isTransitioning = true;

        // Fade out
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            textGroup.alpha = 1f - t;
            yield return null;
        }
        textGroup.alpha = 0f;
        endingText.text = message;

        // Fade in
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
