using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryManager : MonoBehaviour
{
    [Header("스토리 데이터")]
    public StorySet storySet;
    private int currentIndex = 0;

    [Header("UI 연결")]
    public TextMeshProUGUI storyTextUI;
    public Button[] choiceButtons;
    public GameObject nextClickArea;
    public GameObject clickGuide;

    void Start()
    {
        LoadStory(currentIndex);
    }

    public void LoadStory(int index)
    {
        if (index >= storySet.stories.Length)
        {
            Debug.Log("클리어 엔딩 진입");
            // SceneManager.LoadScene("ClearEnding");
            return;
        }

        StoryData data = storySet.stories[index];

        // 방어 코드
        if (data.choices.Length < 3 || data.resultTexts.Length < 3)
        {
            Debug.LogWarning($"index {index}의 선택지 또는 결과 텍스트가 부족합니다.");
            return;
        }

        storyTextUI.text = data.storyText;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int choiceIndex = i;
            choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = data.choices[i];
            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => OnChoice(choiceIndex));
            choiceButtons[i].gameObject.SetActive(true);
        }

        nextClickArea.SetActive(false);
        nextClickArea.GetComponent<Image>().raycastTarget = false;
        clickGuide.SetActive(false);
    }

    void OnChoice(int choiceIndex)
    {
        StoryData data = storySet.stories[currentIndex];

        foreach (var btn in choiceButtons)
            btn.gameObject.SetActive(false);

        storyTextUI.text = data.resultTexts[choiceIndex];

        int moneyDelta = data.moneyChanges[choiceIndex];
        int healthDelta = data.healthChanges[choiceIndex];
        GameManager.Instance.ChangeStatus(moneyDelta, healthDelta);

        FloatingStatusText.Show(moneyDelta, "money");
        FloatingStatusText.Show(healthDelta, "health");

        nextClickArea.SetActive(true);
        nextClickArea.GetComponent<Image>().raycastTarget = true;
        clickGuide.SetActive(true);
    }

    public void NextStory()
    {
        nextClickArea.SetActive(false);
        nextClickArea.GetComponent<Image>().raycastTarget = false;
        clickGuide.SetActive(false);

        if (GameManager.Instance.isGameOver)
        {
            Debug.Log("베드 엔딩 진입");
            // SceneManager.LoadScene("BadEnding");
            return;
        }

        currentIndex++;
        LoadStory(currentIndex);
    }
}
