using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class BadEndingController : MonoBehaviour
{
    [Header("텍스트들 (Hierarchy에서 직접 작성)")]
    public TextMeshProUGUI[] textObjects;      // 0: Bad Ending, 1: 엔딩 문구, 2: 어떠셨나요, 3: 사례
    public CanvasGroup[] canvasGroups;         // 각 텍스트의 CanvasGroup

    [Header("버튼")]
    public CanvasGroup retryButtonGroup;       // 다시 하기
    public CanvasGroup selectButtonGroup;      // 퍼즐 선택

    [Header("페이드 설정")]
    public float fadeDuration = 1f;

    [Header("페이드 아웃용 검은 배경")]
    public CanvasGroup fullscreenPanel;

    private int currentIndex = -1;
    private bool isTransitioning = false;

    void Start()
    {
        // 전부 초기화
        for (int i = 0; i < canvasGroups.Length; i++)
        {
            SetAlpha(canvasGroups[i], 0);
        }

        SetAlpha(retryButtonGroup, 0);
        SetAlpha(selectButtonGroup, 0);
        if (fullscreenPanel != null)
            fullscreenPanel.alpha = 0;

        ShowNext(); // 첫 문장부터 시작
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
            // 이전 문구 FadeOut
            StartCoroutine(FadeOut(canvasGroups[currentIndex - 1]));
        }

        if (currentIndex < canvasGroups.Length)
        {
            // 현재 문구 FadeIn (텍스트는 미리 Hierarchy에서 작성되어 있음)
            StartCoroutine(FadeIn(canvasGroups[currentIndex]));
        }
        else
        {
            // 문장 다 끝났으면 버튼 2개 표시
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

    // 버튼 기능
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
