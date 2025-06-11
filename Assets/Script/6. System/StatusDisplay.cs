/*using UnityEngine;
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
}*/
using UnityEngine;
using TMPro;

public class StatusDisplay : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI healthText;

    private PlayerStatus player;

    void Start()
    {
        player = Object.FindFirstObjectByType<PlayerStatus>();
    }

    void Update()
    {
        if (player == null) return;

        moneyText.text = $"{player.money}";
        healthText.text = $"{player.health}";
    }
}
