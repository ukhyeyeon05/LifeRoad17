using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class NarrationController : MonoBehaviour
{
    public TextMeshProUGUI narrationText;
    public Image clickGuide;
    public float typingSpeed = 0.05f;
    public string[] sentences;

    private int index = 0;
    private bool isTyping = false;

    void Start()
    {
        clickGuide.canvasRenderer.SetAlpha(0f);
        StartCoroutine(ShowSentence());
        StartCoroutine(BlinkArrow());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            if (index < sentences.Length)
            {
                StartCoroutine(ShowSentence());
            }
            else
            {
                SceneManager.LoadScene("ChapterSelect");
            }
        }
    }

    IEnumerator ShowSentence()
    {
        isTyping = true;
        narrationText.text = "";
        string sentence = sentences[index];
        index++;

        foreach (char c in sentence)
        {
            narrationText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    IEnumerator BlinkArrow()
    {
        while (true)
        {
            clickGuide.CrossFadeAlpha(1f, 0.5f, false);
            yield return new WaitForSeconds(0.5f);
            clickGuide.CrossFadeAlpha(0f, 0.5f, false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
