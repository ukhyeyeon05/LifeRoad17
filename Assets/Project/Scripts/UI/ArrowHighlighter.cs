using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowHighlighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject arrowLeft;
    public GameObject arrowRight;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (arrowLeft != null) arrowLeft.SetActive(true);
        if (arrowRight != null) arrowRight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (arrowLeft != null) arrowLeft.SetActive(false);
        if (arrowRight != null) arrowRight.SetActive(false);
    }
}
