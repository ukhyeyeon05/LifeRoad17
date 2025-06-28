using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("��ư Ȯ�� ���")]
    public RectTransform buttonRect;

    [Header("ȭ��ǥ ������Ʈ")]
    public GameObject leftArrow;
    public GameObject rightArrow;

    private Vector3 originalScale;

    void Awake()
    {
        // �ڵ� �Ҵ�
        if (buttonRect == null) buttonRect = GetComponent<RectTransform>();
        if (leftArrow == null) leftArrow = transform.Find("LeftArrow")?.gameObject;
        if (rightArrow == null) rightArrow = transform.Find("RightArrow")?.gameObject;
    }

    void Start()
    {
        originalScale = buttonRect.localScale;

        // ȭ��ǥ �ʱ� ��Ȱ��ȭ
        if (leftArrow != null) leftArrow.SetActive(false);
        if (rightArrow != null) rightArrow.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ��ư Ȯ��
        buttonRect.localScale = originalScale * 1.1f;

        // ȭ��ǥ ǥ��
        if (leftArrow != null) leftArrow.SetActive(true);
        if (rightArrow != null) rightArrow.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���� ũ��� ����
        buttonRect.localScale = originalScale;

        // ȭ��ǥ ����
        if (leftArrow != null) leftArrow.SetActive(false);
        if (rightArrow != null) rightArrow.SetActive(false);
    }
}
