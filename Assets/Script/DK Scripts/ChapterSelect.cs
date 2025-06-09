using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChapterSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Vector3 originalScale;
    public int puzzleID;
    private Button button;

    void Start()
    {
        originalScale = transform.localScale;
        button = GetComponent<Button>();

        int cleared = PlayerPrefs.GetInt($"SDGsNo.{puzzleID}", 0);
        Debug.Log($"[퍼즐 {puzzleID}] 클리어 여부: {cleared}");

        if (cleared == 1)
        {
            button.interactable = false;

            ColorBlock cb = button.colors;
            cb.normalColor = Color.gray;
            cb.highlightedColor = Color.gray;
            cb.pressedColor = Color.gray;
            button.colors = cb;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debug.Log("PlayerPrefs 초기화 완료");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (button.interactable)
            transform.localScale = originalScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!button.interactable) return;

        PlayerPrefs.SetInt("선택된 주제", puzzleID);
        PlayerPrefs.Save();

        string nextSceneName = $"Puz{puzzleID}_Starting";
        SceneManager.LoadScene(nextSceneName);
    }
}
