using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeToScene : MonoBehaviour
{
    public Image fadePanel;          // 검정 패널 이미지
    public float fadeDuration = 1f;  // 페이드 시간

    public void StartGame()
    {
        // 나레이션을 보여줄 수 있도록 초기화
        PlayerPrefs.SetInt("isIntroDone", 0);
        PlayerPrefs.Save();

        StartCoroutine(FadeAndLoad("0_ChapterSelect"));
    }

    IEnumerator FadeAndLoad(string sceneName)
    {
        fadePanel.gameObject.SetActive(true);

        float elapsed = 0f;
        Color c = fadePanel.color;
        c.a = 0f;
        fadePanel.color = c;

        // 페이드 아웃 (Alpha 0 → 1)
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration);
            fadePanel.color = c;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
