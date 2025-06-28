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
        Debug.Log($"[���� {puzzleID}] Ŭ���� ����: {cleared}");

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

        PlayerPrefs.SetInt("���õ� ����", puzzleID);
        PlayerPrefs.SetString("LastPuzzleScene", $"Puz{puzzleID}_Starting");
        PlayerPrefs.Save();

        string nextSceneName = $"Puz{puzzleID}_Starting";
        SceneManager.LoadScene(nextSceneName);
    }
}
