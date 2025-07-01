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
        panelIntroText.SetActive(true);       // 소개 먼저 보여주기
        panelChoice.SetActive(false);
        questionText.gameObject.SetActive(false);
        resultPanel.SetActive(false);
        waitingForClickToContinue = false;
    }

    void ShowCurrentStory()
    {
        if (currentIndex >= storySet.storyList.Count)
        {
            Debug.Log("스토리 끝!");
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

        // 1. 상태 반영
        if (playerStatus != null &&
            data.choiceEffects != null &&
            selectedIndex < data.choiceEffects.Length &&
            data.choiceEffects[selectedIndex] != null)
        {
            playerStatus.ApplyEffect(data.choiceEffects[selectedIndex].effects);
        }

        // 2. 질문/선택지 숨기고 결과 보여주기
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
    public StorySet storySet; // ScriptableObject 연결
    private int currentIndex = 0;
    private bool waitingForNext = false;

    public PlayerStatus playerStatus;

    public GameObject panelIntroText;
    public GameObject panelChoice;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] choiceTexts; // 3개
    public GameObject[] choiceButtons;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private void Start()
    {
        panelIntroText.SetActive(true);     // 인트로 먼저 보여줌
        panelChoice.SetActive(false);       // 선택지는 숨김
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

        // 핵심 요소가 0 이하가 되면 즉시 엔딩으로 전환
        if (playerStatus.money <= 0 || playerStatus.health <= 0)
        {
            PlayerPrefs.SetInt("money", playerStatus.money);
            PlayerPrefs.SetInt("health", playerStatus.health);
            PlayerPrefs.SetString("puzzleId", "puz1");
            PlayerPrefs.SetString("endingType", "Bad");

            SceneManager.LoadScene("EndingScene");
            return; // 이후 코드 실행하지 않도록
        }

        // 결과 출력 흐름
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
            // 핵심 요소 기준 엔딩 판단
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
