/*using UnityEngine;
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
}*/
/*using UnityEngine;
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
        // ���� ���� �� �̸� ���� (BedEnding���� �ٽý��ۿ�)
        PlayerPrefs.SetString("LastPuzzleScene", SceneManager.GetActiveScene().name);
        LoadStory(currentIndex);
    }

    public void LoadStory(int index)
    {
        if (index >= storySet.stories.Length)
        {
            Debug.Log("Ŭ���� ���� ����");

            int puzzleID = PlayerPrefs.GetInt("���õ� ����", 0);
            Debug.Log($"���� {puzzleID} �Ϸ� ó��");

            PlayerPrefs.SetInt($"SDGsNo.{puzzleID}", 1);
            PlayerPrefs.SetInt("isIntroDone", 1);
            PlayerPrefs.Save();

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
        // ���� ���� �� �ش� �� �̸� ����
        PlayerPrefs.SetString("LastPuzzleScene", SceneManager.GetActiveScene().name);
        LoadStory(currentIndex);
    }

    public void LoadStory(int index)
    {
        if (index >= storySet.stories.Length)
        {
            int puzzleID = PlayerPrefs.GetInt("���õ� ����", 0);
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
    [Header("���丮 ������")]
    public StorySet storySet;
    private int currentIndex = 0;

    [Header("UI ����")]
    public TextMeshProUGUI storyTextUI;
    public Button[] choiceButtons;
    public GameObject nextClickArea;
    public GameObject clickGuide;

    [Header("��Ʈ�� ���� UI")]
    public GameObject introPanel; // ��/�ǰ� ���� �г�

    private bool hasSeenIntro = false;

    void Start()
    {
        // ���� ���� �� �̸� ����
        PlayerPrefs.SetString("LastPuzzleScene", SceneManager.GetActiveScene().name);

        // ��Ʈ�� �г� ���� �����ְ� �� ���丮�� ����
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
            Debug.Log("Ŭ���� ���� ����");

            int puzzleID = PlayerPrefs.GetInt("���õ� ����", 0);
            PlayerPrefs.SetInt($"SDGsNo.{puzzleID}", 1);
            PlayerPrefs.SetInt("isIntroDone", 1);
            PlayerPrefs.Save();

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

    // FullscreenPanel Ŭ���� �Լ�
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
    [Header("���丮 ������")]
    public StorySet storySet;
    private int currentIndex = 0;

    [Header("UI ����")]
    public TextMeshProUGUI storyTextUI;
    public Button[] choiceButtons;
    public GameObject nextClickArea;
    public GameObject clickGuide;

    [Header("��Ʈ�� ���� UI")]
    public GameObject introPanel; // ��/�ǰ� �Ұ� �г�

    private bool hasSeenIntro = false;

    void Start()
    {
        // ���� ���� �� �̸� ����
        PlayerPrefs.SetString("LastPuzzleScene", SceneManager.GetActiveScene().name);

        // ��Ʈ�� ���� �����ְ� �� ���丮�� ����
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
            Debug.Log("Ŭ���� ���� ����");

            int puzzleID = PlayerPrefs.GetInt("���õ� ����", 0);
            PlayerPrefs.SetInt($"SDGsNo.{puzzleID}", 1);
            PlayerPrefs.SetInt("isIntroDone", 1);
            PlayerPrefs.Save();

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

    // FullscreenPanel Ŭ�� ó��
    public void OnClickFullscreen()
    {
        if (!hasSeenIntro)
        {
            hasSeenIntro = true;
            introPanel.SetActive(false);  // ���� �г� �����
            LoadStory(0);                 // �������� ���丮 ����
        }
        else
        {
            NextStory();                  // ����ó�� ���� ���丮�� �̵�
        }
    }
}
