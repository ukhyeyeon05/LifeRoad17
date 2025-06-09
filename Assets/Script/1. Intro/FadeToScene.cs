using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeToScene : MonoBehaviour
{
    public Image fadePanel;          // ���� �г� �̹���
    public float fadeDuration = 1f;  // ���̵� �ð�

    public void StartGame()
    {
        // �����̼��� ������ �� �ֵ��� �ʱ�ȭ
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

        // ���̵� �ƿ� (Alpha 0 �� 1)
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
