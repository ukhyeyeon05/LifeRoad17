using UnityEngine;
using UnityEngine.EventSystems;

public class StarTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltipTextObj; // 별 아래 텍스트 오브젝트 할당

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (tooltipTextObj != null)
            tooltipTextObj.SetActive(true); // 마우스 올리면 표시
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipTextObj != null)
            tooltipTextObj.SetActive(false); // 벗어나면 숨김
    }
}
