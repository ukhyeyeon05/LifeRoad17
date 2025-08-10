/*using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public StorySet storySet;
    private int currentIndex = 0;
    private bool waitingForNext = false;

    public PlayerStatus playerStatus;

    public GameObject panelIntroText;
    public GameObject panelChoice;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] choiceTexts;
    public GameObject[] choiceButtons;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private string puzzleId = "";

    private void Start()
    {
        panelIntroText.SetActive(true);
        panelChoice.SetActive(false);

        PuzzleIdSetter setter = Object.FindFirstObjectByType<PuzzleIdSetter>();
        if (setter != null)
        {
            puzzleId = setter.puzzleId;
            PlayerPrefs.SetString("puzzleId", puzzleId);
        }

        // 저장된 진행 불러오기
        if (SaveSystem.HasSavedGame())
        {
            SaveData data = SaveSystem.LoadGame();
            if (data != null && data.puzzleId == puzzleId)
            {
                currentIndex = data.currentIndex;
                playerStatus.money = data.money;
                playerStatus.health = data.health;
                Debug.Log($"[StoryManager] 진행 복원 완료: index={currentIndex}, 돈={data.money}, 건강={data.health}");
            }
        }

        DisplayCurrentStory();
    }

    public void OnIntroClick()
    {
        panelIntroText.SetActive(false);
        panelChoice.SetActive(true);
        DisplayCurrentStory();
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        StatusEffect[] effects = storySet.stories[currentIndex].choiceEffects[choiceIndex].effects;
        playerStatus.ApplyEffect(effects);

        if (playerStatus.money <= 0 || playerStatus.health <= 0)
        {
            SaveBadEnding();
            return;
        }

        panelChoice.SetActive(false);

        string result = storySet.stories[currentIndex].results[choiceIndex];
        resultPanel.SetActive(true);
        resultText.text = result;

        currentIndex++;
        waitingForNext = true;

        SaveProgress(); // 저장
    }

    private void SaveProgress()
    {
        SaveData data = new SaveData
        {
            puzzleId = puzzleId,
            currentIndex = currentIndex,
            money = playerStatus.money,
            health = playerStatus.health
        };

        SaveSystem.SaveGame(data);
    }

    private void SaveBadEnding()
    {
        PlayerPrefs.SetInt("money", playerStatus.money);
        PlayerPrefs.SetInt("health", playerStatus.health);
        PlayerPrefs.SetString("endingType", "Bad");
        PlayerPrefs.SetString("puzzleId", puzzleId);

        SaveSystem.ClearSavedGame();
        SceneManager.LoadScene("EndingScene");
    }

    private void NextStep()
    {
        resultPanel.SetActive(false);

        if (currentIndex >= storySet.stories.Count)
        {
            bool isBad = playerStatus.money <= 0 || playerStatus.health <= 0;

            PlayerPrefs.SetInt("money", playerStatus.money);
            PlayerPrefs.SetInt("health", playerStatus.health);
            PlayerPrefs.SetString("endingType", isBad ? "Bad" : "Normal");
            PlayerPrefs.SetString("puzzleId", puzzleId);

            SaveSystem.ClearSavedGame();
            SceneManager.LoadScene("EndingScene");
        }
        else
        {
            DisplayCurrentStory();
        }
    }

    private void DisplayCurrentStory()
    {
        var current = storySet.stories[currentIndex];

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

            if (currentIndex >= storySet.stories.Count)
            {
                bool isBad = playerStatus.money <= 0 || playerStatus.health <= 0;
                PlayerPrefs.SetString("endingType", isBad ? "Bad" : "Normal");
                PlayerPrefs.SetString("puzzleId", puzzleId);
                SaveSystem.ClearSavedGame();
                SceneManager.LoadScene("EndingScene");
            }
            else
            {
                panelChoice.SetActive(true);
                DisplayCurrentStory();
            }
        }
    }
}*/
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public StorySet storySet;
    private int currentIndex = 0;
    private bool waitingForNext = false;

    public PlayerStatus playerStatus;

    public GameObject panelIntroText;
    public GameObject panelChoice;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] choiceTexts;
    public GameObject[] choiceButtons;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private string puzzleId = "";

    private void Start()
    {
        panelIntroText.SetActive(true);
        panelChoice.SetActive(false);

        PuzzleIdSetter setter = Object.FindFirstObjectByType<PuzzleIdSetter>();
        if (setter != null)
        {
            puzzleId = setter.puzzleId;
            PlayerPrefs.SetString("puzzleId", puzzleId);
        }

        // 저장된 진행 불러오기
        if (SaveSystem.HasSavedGame())
        {
            SaveData data = SaveSystem.LoadGame();
            if (data != null && data.puzzleId == puzzleId)
            {
                currentIndex = data.currentIndex;
                playerStatus.money = data.money;
                playerStatus.health = data.health;
                Debug.Log($"[StoryManager] 진행 복원 완료: index={currentIndex}, 돈={data.money}, 건강={data.health}");
            }
        }

        DisplayCurrentStory();
    }

    public void OnIntroClick()
    {
        panelIntroText.SetActive(false);
        panelChoice.SetActive(true);
        DisplayCurrentStory();
    }

    public void OnChoiceSelected(int choiceIndex)
    {
        StatusEffect[] effects = storySet.stories[currentIndex].choiceEffects[choiceIndex].effects;
        playerStatus.ApplyEffect(effects);

        if (playerStatus.money <= 0 || playerStatus.health <= 0)
        {
            PlayerPrefs.SetInt("money", playerStatus.money);
            PlayerPrefs.SetInt("health", playerStatus.health);
            PlayerPrefs.SetString("endingType", "Bad");
            PlayerPrefs.SetString("puzzleId", puzzleId);

            SaveSystem.ClearSavedGame();
            SceneManager.LoadScene("EndingScene");
            return;
        }

        panelChoice.SetActive(false);

        string result = storySet.stories[currentIndex].results[choiceIndex];
        resultPanel.SetActive(true);
        resultText.text = result;

        currentIndex++;
        waitingForNext = true;

        SaveProgress(); // 저장
    }

    void SaveProgress()
    {
        SaveData data = new SaveData
        {
            puzzleId = puzzleId,
            currentIndex = currentIndex,
            money = playerStatus.money,
            health = playerStatus.health
        };

        SaveSystem.SaveGame(data);
    }

    private void NextStep()
    {
        resultPanel.SetActive(false);

        if (currentIndex >= storySet.stories.Count)
        {
            bool isBad = playerStatus.money <= 0 || playerStatus.health <= 0;

            PlayerPrefs.SetInt("money", playerStatus.money);
            PlayerPrefs.SetInt("health", playerStatus.health);
            PlayerPrefs.SetString("endingType", isBad ? "Bad" : "Normal");
            PlayerPrefs.SetString("puzzleId", puzzleId);

            SaveSystem.ClearSavedGame();
            SceneManager.LoadScene("EndingScene");
        }
        else
        {
            DisplayCurrentStory();
        }
    }

    private void DisplayCurrentStory()
    {
        var current = storySet.stories[currentIndex];

        questionText.text = current.question;

        for (int i = 0; i < choiceTexts.Length; i++)
        {
            choiceTexts[i].text = current.choices[i];
            choiceButtons[i].SetActive(true);
        }

        // 수치 UI 갱신
        ChoiceEffectDisplay choiceEffectDisplay = Object.FindFirstObjectByType<ChoiceEffectDisplay>();
        if (choiceEffectDisplay != null)
        {
            choiceEffectDisplay.UpdateEffectUI(currentIndex);
        }
    }

    private void Update()
    {
        if (resultPanel.activeSelf && Input.GetMouseButtonDown(0) && waitingForNext)
        {
            waitingForNext = false;
            resultPanel.SetActive(false);

            if (currentIndex >= storySet.stories.Count)
            {
                bool isBad = playerStatus.money <= 0 || playerStatus.health <= 0;
                PlayerPrefs.SetString("endingType", isBad ? "Bad" : "Normal");
                PlayerPrefs.SetString("puzzleId", puzzleId);
                SaveSystem.ClearSavedGame();
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

