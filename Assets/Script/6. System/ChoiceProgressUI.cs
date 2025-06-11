using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceProgressUI : MonoBehaviour
{
    public TextMeshProUGUI choiceText;
    public Image[] dots;
    public Sprite filledDot; // ���õ� ��
    public Sprite emptyDot;  // ���� �ȵ� ��

    public void UpdateProgress(int current)
    {
        choiceText.text = $"Choice {current} / 10";
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].sprite = (i < current) ? filledDot : emptyDot;
        }
    }
}
