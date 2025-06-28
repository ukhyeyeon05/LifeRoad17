using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class ClearEndingController : MonoBehaviour
{
    public TextMeshProUGUI[] textObjects;      // Hierarchy에 있는 TMP 텍스트
    public CanvasGroup[] canvasGroups;         // 그에 대응되는 CanvasGroup
    public CanvasGroup buttonGroup;            // [다른 퍼즐 보기] 버튼
    public CanvasGroup fullscreenPanel;        // 페이드 아웃용 검은 패널

    public float fadeDuration = 1f;
    private int currentIndex = -1;
    private bool isTransitioning = false;

    void Start()
    {
        // 처음엔 다 숨김
        for (int i = 0; i < canvasGroups.Length; i++)
        {
            SetAlpha(canvasGroups[i], 0);
        }

        SetAlpha(buttonGroup, 0);
        if (fullscreenPanel != null)
            fullscreenPanel.alpha = 0;

        ShowNext(); // 첫 문장 보여주기
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
            // 현재 문구 FadeIn
            StartCoroutine(FadeIn(canvasGroups[currentIndex]));
        }
        else
        {
            StartCoroutine(FadeIn(buttonGroup)); // 마지막엔 버튼 등장
        }
    }

    IEnumerator FadeIn(CanvasGroup group)
    {
        isTransitioning = true;
        float time = 0f;
        while (time < fadeDuration)
        {
            group.alpha = Mathf.Lerp(0, 1, time / fadeDuration);
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
            group.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }
        SetAlpha(group, 0f);
        isTransitioning = false;
    }

    void SetAlpha(CanvasGroup group, float alpha)
    {
        group.alpha = alpha;
        group.interactable = alpha >= 1;
        group.blocksRaycasts = alpha >= 1;
    }

    public void OnClickNextPuzzle()
    {
        StartCoroutine(FadeOutAndLoad("0_ChapterSelect"));
    }

    IEnumerator FadeOutAndLoad(string sceneName)
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

        fullscreenPanel.alpha = 1f;
        SceneManager.LoadScene(sceneName);
    }
}

