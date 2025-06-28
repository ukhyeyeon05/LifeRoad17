using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class BadEndingController : MonoBehaviour
{
    [Header("�ؽ�Ʈ�� (Hierarchy���� ���� �ۼ�)")]
    public TextMeshProUGUI[] textObjects;      // 0: Bad Ending, 1: ���� ����, 2: ��̳���, 3: ���
    public CanvasGroup[] canvasGroups;         // �� �ؽ�Ʈ�� CanvasGroup

    [Header("��ư")]
    public CanvasGroup retryButtonGroup;       // �ٽ� �ϱ�
    public CanvasGroup selectButtonGroup;      // ���� ����

    [Header("���̵� ����")]
    public float fadeDuration = 1f;

    [Header("���̵� �ƿ��� ���� ���")]
    public CanvasGroup fullscreenPanel;

    private int currentIndex = -1;
    private bool isTransitioning = false;

    void Start()
    {
        // ���� �ʱ�ȭ
        for (int i = 0; i < canvasGroups.Length; i++)
        {
            SetAlpha(canvasGroups[i], 0);
        }

        SetAlpha(retryButtonGroup, 0);
        SetAlpha(selectButtonGroup, 0);
        if (fullscreenPanel != null)
            fullscreenPanel.alpha = 0;

        ShowNext(); // ù ������� ����
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTransitioning)
        {
            ShowNext();
        }
    }

    void ShowNext()
    {
        currentIndex++;

        if (currentIndex > 0 && currentIndex <= canvasGroups.Length)
        {
            // ���� ���� FadeOut
            StartCoroutine(FadeOut(canvasGroups[currentIndex - 1]));
        }

        if (currentIndex < canvasGroups.Length)
        {
            // ���� ���� FadeIn (�ؽ�Ʈ�� �̸� Hierarchy���� �ۼ��Ǿ� ����)
            StartCoroutine(FadeIn(canvasGroups[currentIndex]));
        }
        else
        {
            // ���� �� �������� ��ư 2�� ǥ��
            StartCoroutine(FadeIn(retryButtonGroup));
            StartCoroutine(FadeIn(selectButtonGroup));
        }
    }

    IEnumerator FadeIn(CanvasGroup group)
    {
        isTransitioning = true;
        float time = 0f;
        while (time < fadeDuration)
        {
            group.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        SetAlpha(group, 1f);
        isTransitioning = false;
    }

    IEnumerator FadeOut(CanvasGroup group)
    {
        isTransitioning = true;
        float time = 0f;
        while (time < fadeDuration)
        {
            group.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        SetAlpha(group, 0f);
        isTransitioning = false;
    }

    void SetAlpha(CanvasGroup group, float alpha)
    {
        group.alpha = alpha;
        group.interactable = (alpha >= 1);
        group.blocksRaycasts = (alpha >= 1);
    }

    // ��ư ���
    public void RetryPuzzle()
    {
        string lastScene = PlayerPrefs.GetString("LastPuzzleScene", "Puz1_Starting");
        SceneManager.LoadScene(lastScene);
    }

    public void GoToPuzzleSelect()
    {
        StartCoroutine(FadeOutAndLoad("0_ChapterSelect"));
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        if (fullscreenPanel != null)
        {
            fullscreenPanel.gameObject.SetActive(true);
            float time = 0f;
            float duration = 1f;
            while (time < duration)
            {
                fullscreenPanel.alpha = Mathf.Lerp(0f, 1f, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
        }

        SceneManager.LoadScene(sceneName);
    }
}
