using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceProgressUI : MonoBehaviour
{
    public TextMeshProUGUI choiceText;
    public Image[] dots;
    public Sprite filledDot; // 선택된 ●
    public Sprite emptyDot;  // 선택 안된 ○

    public void UpdateProgress(int current)
    {
        choiceText.text = $"Choice {current} / 10";
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].sprite = (i < current) ? filledDot : emptyDot;
        }
    }
}
