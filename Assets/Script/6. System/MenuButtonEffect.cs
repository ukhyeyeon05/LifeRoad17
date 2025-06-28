using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("버튼 확대 대상")]
    public RectTransform buttonRect;

    [Header("화살표 오브젝트")]
    public GameObject leftArrow;
    public GameObject rightArrow;

    private Vector3 originalScale;

    void Awake()
    {
        // 자동 할당
        if (buttonRect == null) buttonRect = GetComponent<RectTransform>();
        if (leftArrow == null) leftArrow = transform.Find("LeftArrow")?.gameObject;
        if (rightArrow == null) rightArrow = transform.Find("RightArrow")?.gameObject;
    }

    void Start()
    {
        originalScale = buttonRect.localScale;

        // 화살표 초기 비활성화
        if (leftArrow != null) leftArrow.SetActive(false);
        if (rightArrow != null) rightArrow.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 버튼 확대
        buttonRect.localScale = originalScale * 1.1f;

        // 화살표 표시
        if (leftArrow != null) leftArrow.SetActive(true);
        if (rightArrow != null) rightArrow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 원래 크기로 복귀
        buttonRect.localScale = originalScale;

        // 화살표 숨김
        if (leftArrow != null) leftArrow.SetActive(false);
        if (rightArrow != null) rightArrow.SetActive(false);
    }
}
