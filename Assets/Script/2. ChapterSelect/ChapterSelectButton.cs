using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChapterSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int puzzleID;
    private Vector3 originalScale;
    private Button button;

    void Start()
    {
        originalScale = transform.localScale;
        button = GetComponent<Button>();

        // 이미 클리어한 퍼즐이면 비활성화 + 색상 회색
        if (PlayerPrefs.GetInt($"SDGsNo.{puzzleID}", 0) == 1)
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

        PlayerPrefs.SetInt("선택된 주제", puzzleID);
        PlayerPrefs.Save();
        SceneManager.LoadScene("1_ChapterInfo"); 
    }
}
