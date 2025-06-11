/*using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

            // 퍼즐 클리어 정보 저장
            int puzzleID = PlayerPrefs.GetInt("선택된 주제", 0);
            Debug.Log($"퍼즐 {puzzleID} 완료 처리");

            PlayerPrefs.SetInt($"SDGsNo.{puzzleID}", 1);
            PlayerPrefs.SetInt("isIntroDone", 1);
            PlayerPrefs.Save();

            // 퍼즐 전체 클리어 시 최종 엔딩도 고려 가능
            SceneManager.LoadScene("ClearEnding1");
            return;
        }

        StoryData data = storySet.stories[index];

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
}*/
/*using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
        // 현재 퍼즐 씬 이름 저장 (BedEnding에서 다시시작용)
        PlayerPrefs.SetString("LastPuzzleScene", SceneManager.GetActiveScene().name);
        LoadStory(currentIndex);
    }

    public void LoadStory(int index)
    {
        if (index >= storySet.stories.Length)
        {
            Debug.Log("클리어 엔딩 진입");

            int puzzleID = PlayerPrefs.GetInt("선택된 주제", 0);
            Debug.Log($"퍼즐 {puzzleID} 완료 처리");

            PlayerPrefs.SetInt($"SDGsNo.{puzzleID}", 1);
            PlayerPrefs.SetInt("isIntroDone", 1);
            PlayerPrefs.Save();

            SceneManager.LoadScene("ClearEnding1");
            return;
        }

        StoryData data = storySet.stories[index];

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
            SceneManager.LoadScene("BedEnding");
            return;
        }

        currentIndex++;
        LoadStory(currentIndex);
    }
}*/
/*using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public StorySet storySet;
    private int currentIndex = 0;

    public TextMeshProUGUI storyTextUI;
    public Button[] choiceButtons;
    public GameObject nextClickArea;
    public GameObject clickGuide;

    void Start()
    {
        // 퍼즐 시작 시 해당 씬 이름 저장
        PlayerPrefs.SetString("LastPuzzleScene", SceneManager.GetActiveScene().name);
        LoadStory(currentIndex);
    }

    public void LoadStory(int index)
    {
        if (index >= storySet.stories.Length)
        {
            int puzzleID = PlayerPrefs.GetInt("선택된 주제", 0);
            PlayerPrefs.SetInt($"SDGsNo.{puzzleID}", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("ClearEnding1");
            return;
        }

        StoryData data = storySet.stories[index];
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

        currentIndex++;
        LoadStory(currentIndex);
    }
}*/
/*using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    [Header("인트로 설명 UI")]
    public GameObject introPanel; // 돈/건강 설명 패널

    private bool hasSeenIntro = false;

    void Start()
    {
        // 현재 퍼즐 씬 이름 저장
        PlayerPrefs.SetString("LastPuzzleScene", SceneManager.GetActiveScene().name);

        // 인트로 패널 먼저 보여주고 본 스토리는 숨김
        introPanel.SetActive(true);
        storyTextUI.gameObject.SetActive(false);
        foreach (var btn in choiceButtons)
            btn.gameObject.SetActive(false);

        nextClickArea.SetActive(true);
        clickGuide.SetActive(true);
    }

    public void LoadStory(int index)
    {
        if (index >= storySet.stories.Length)
        {
            Debug.Log("클리어 엔딩 진입");

            int puzzleID = PlayerPrefs.GetInt("선택된 주제", 0);
            PlayerPrefs.SetInt($"SDGsNo.{puzzleID}", 1);
            PlayerPrefs.SetInt("isIntroDone", 1);
            PlayerPrefs.Save();

            SceneManager.LoadScene("ClearEnding1");
            return;
        }

        StoryData data = storySet.stories[index];

        if (data.choices.Length < 3 || data.resultTexts.Length < 3)
        {
            Debug.LogWarning($"index {index}의 선택지 또는 결과 텍스트가 부족합니다.");
            return;
        }

        storyTextUI.text = data.storyText;
        storyTextUI.gameObject.SetActive(true);

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

        currentIndex++;
        LoadStory(currentIndex);
    }

    // FullscreenPanel 클릭용 함수
    public void OnClickFullscreen()
    {
        if (!hasSeenIntro)
        {
            hasSeenIntro = true;
            introPanel.SetActive(false);
            LoadStory(0);
        }
        else
        {
            NextStory();
        }
    }
}
*/

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    [Header("인트로 설명 UI")]
    public GameObject introPanel; // 돈/건강 소개 패널

    private bool hasSeenIntro = false;

    void Start()
    {
        // 현재 퍼즐 씬 이름 저장
        PlayerPrefs.SetString("LastPuzzleScene", SceneManager.GetActiveScene().name);

        // 인트로 먼저 보여주고 본 스토리는 숨김
        introPanel.SetActive(true);
        storyTextUI.gameObject.SetActive(false);
        foreach (var btn in choiceButtons)
            btn.gameObject.SetActive(false);

        nextClickArea.SetActive(true);
        clickGuide.SetActive(true);
    }

    public void LoadStory(int index)
    {
        if (index >= storySet.stories.Length)
        {
            Debug.Log("클리어 엔딩 진입");

            int puzzleID = PlayerPrefs.GetInt("선택된 주제", 0);
            PlayerPrefs.SetInt($"SDGsNo.{puzzleID}", 1);
            PlayerPrefs.SetInt("isIntroDone", 1);
            PlayerPrefs.Save();

            SceneManager.LoadScene("ClearEnding1");
            return;
        }

        StoryData data = storySet.stories[index];

        if (data.choices.Length < 3 || data.resultTexts.Length < 3)
        {
            Debug.LogWarning($"index {index}의 선택지 또는 결과 텍스트가 부족합니다.");
            return;
        }

        storyTextUI.text = data.storyText;
        storyTextUI.gameObject.SetActive(true);

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

        currentIndex++;
        LoadStory(currentIndex);
    }

    // FullscreenPanel 클릭 처리
    public void OnClickFullscreen()
    {
        if (!hasSeenIntro)
        {
            hasSeenIntro = true;
            introPanel.SetActive(false);  // 설명 패널 숨기기
            LoadStory(0);                 // 본격적인 스토리 시작
        }
        else
        {
            NextStory();                  // 기존처럼 다음 스토리로 이동
        }
    }
}
