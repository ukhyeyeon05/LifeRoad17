using UnityEngine;
using TMPro;

//상태 변화가 일어날 때마다 텍스트 갱신
public class StatusDisplay : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI healthText;

    void Update()
    {
        moneyText.text = $"{GameManager.Instance.money}";
        healthText.text = $"{GameManager.Instance.health}";
    }
}
