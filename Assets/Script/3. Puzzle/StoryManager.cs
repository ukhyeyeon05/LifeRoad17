using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    [Header("���丮 ������")]
    public StorySet storySet;
    private int currentIndex = 0;

    [Header("UI ����")]
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
            Debug.Log("Ŭ���� ���� ����");

            // ���� Ŭ���� ���� ����
            int puzzleID = PlayerPrefs.GetInt("���õ� ����", 0);
            Debug.Log($"���� {puzzleID} �Ϸ� ó��");

            PlayerPrefs.SetInt($"SDGsNo.{puzzleID}", 1);
            PlayerPrefs.SetInt("isIntroDone", 1);
            PlayerPrefs.Save();

            // ���� ��ü Ŭ���� �� ���� ������ ��� ����
            SceneManager.LoadScene("ClearEnding1");
            return;
        }

        StoryData data = storySet.stories[index];

        if (data.choices.Length < 3 || data.resultTexts.Length < 3)
        {
            Debug.LogWarning($"index {index}�� ������ �Ǵ� ��� �ؽ�Ʈ�� �����մϴ�.");
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
            Debug.Log("���� ���� ����");
            // SceneManager.LoadScene("BadEnding");
            return;
        }

        currentIndex++;
        LoadStory(currentIndex);
    }
}
