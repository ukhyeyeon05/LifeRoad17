/*using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryManager : MonoBehaviour
{
    public StorySet storySet;
    public PlayerStatus playerStatus;

    public GameObject panelIntroText;
    public GameObject panelChoice;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] choiceTexts;
    public Button[] choiceButtons;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int currentIndex = 0;
    private bool waitingForClickToContinue = false;
    private bool hasIntroEnded = false;

    void Start()
    {
        panelIntroText.SetActive(true);       // �Ұ� ���� �����ֱ�
        panelChoice.SetActive(false);
        questionText.gameObject.SetActive(false);
        resultPanel.SetActive(false);
        waitingForClickToContinue = false;
    }

    void ShowCurrentStory()
    {
        if (currentIndex >= storySet.storyList.Count)
        {
            Debug.Log("���丮 ��!");
            return;
        }

        var data = storySet.storyList[currentIndex];

        questionText.text = data.question;
        for (int i = 0; i < choiceTexts.Length; i++)
        {
            choiceTexts[i].text = data.choices[i];
            int capturedIndex = i;
            choiceButtons[i].onClick.RemoveAllListeners();
            choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(capturedIndex));
        }

        panelChoice.SetActive(true);
        questionText.gameObject.SetActive(true);
        resultPanel.SetActive(false);
        waitingForClickToContinue = false;
    }

    void OnChoiceSelected(int selectedIndex)
    {
        var data = storySet.storyList[currentIndex];

        // 1. ���� �ݿ�
        if (playerStatus != null &&
            data.choiceEffects != null &&
            selectedIndex < data.choiceEffects.Length &&
            data.choiceEffects[selectedIndex] != null)
        {
            playerStatus.ApplyEffect(data.choiceEffects[selectedIndex].effects);
        }

        // 2. ����/������ ����� ��� �����ֱ�
        panelChoice.SetActive(false);
        questionText.gameObject.SetActive(false);

        resultPanel.SetActive(true);
        resultText.text = data.results[selectedIndex];

        waitingForClickToContinue = true;
    }

    void Update()
    {
        if (!hasIntroEnded && Input.GetMouseButtonDown(0))
        {
            hasIntroEnded = true;
            panelIntroText.SetActive(false);
            ShowCurrentStory();
        }
        else if (waitingForClickToContinue && Input.GetMouseButtonDown(0))
        {
            currentIndex++;
            ShowCurrentStory();
        }
    }
}*/
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public StorySet storySet; // ScriptableObject ����
    private int currentIndex = 0;
    private bool waitingForNext = false;

    public PlayerStatus playerStatus;

    public GameObject panelIntroText;
    public GameObject panelChoice;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] choiceTexts; // 3��
    public GameObject[] choiceButtons;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private void Start()
    {
        panelIntroText.SetActive(true);     // ��Ʈ�� ���� ������
        panelChoice.SetActive(false);       // �������� ����
    }

    public void OnIntroClick()
    {
        panelIntroText.SetActive(false);
        panelChoice.SetActive(true);
        DisplayCurrentStory();
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        StatusEffect[] effects = storySet.storyList[currentIndex].choiceEffects[choiceIndex].effects;
        playerStatus.ApplyEffect(effects);

        // �ٽ� ��Ұ� 0 ���ϰ� �Ǹ� ��� �������� ��ȯ
        if (playerStatus.money <= 0 || playerStatus.health <= 0)
        {
            PlayerPrefs.SetInt("money", playerStatus.money);
            PlayerPrefs.SetInt("health", playerStatus.health);
            PlayerPrefs.SetString("puzzleId", "puz1");
            PlayerPrefs.SetString("endingType", "Bad");

            SceneManager.LoadScene("EndingScene");
            return; // ���� �ڵ� �������� �ʵ���
        }

        // ��� ��� �帧
        panelChoice.SetActive(false);

        string result = storySet.storyList[currentIndex].results[choiceIndex];
        resultPanel.SetActive(true);
        resultText.text = result;

        currentIndex++;
        waitingForNext = true;
    }



    private void NextStep()
    {
        resultPanel.SetActive(false);

        if (currentIndex >= storySet.storyList.Count)
        {
            // �ٽ� ��� ���� ���� �Ǵ�
            bool isBad = playerStatus.money <= 0 || playerStatus.health <= 0;

            PlayerPrefs.SetInt("money", playerStatus.money);
            PlayerPrefs.SetInt("health", playerStatus.health);
            PlayerPrefs.SetString("puzzleId", "puz1");
            PlayerPrefs.SetString("endingType", isBad ? "Bad" : "Normal");

            SceneManager.LoadScene("EndingScene");

        }
        else
        {
            DisplayCurrentStory();
        }
    }




    private void DisplayCurrentStory()
    {
        var current = storySet.storyList[currentIndex];

        questionText.text = current.question;

        for (int i = 0; i < choiceTexts.Length; i++)
        {
            choiceTexts[i].text = current.choices[i];
            choiceButtons[i].SetActive(true);
        }
    }

    
    private void Update()
    {
        if (resultPanel.activeSelf && Input.GetMouseButtonDown(0) && waitingForNext)
        {
            waitingForNext = false;
            resultPanel.SetActive(false);

            if (currentIndex >= storySet.storyList.Count)
            {
                bool isBad = playerStatus.money <= 0 || playerStatus.health <= 0;
                PlayerPrefs.SetString("puzzleId", "puz1");
                PlayerPrefs.SetString("endingType", isBad ? "Bad" : "Normal");
                SceneManager.LoadScene("EndingScene");
            }
            else
            {
                panelChoice.SetActive(true);
                DisplayCurrentStory();
            }
        }
    }

}
