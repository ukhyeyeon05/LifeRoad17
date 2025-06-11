/*using UnityEngine;
using TMPro;

//���� ��ȭ�� �Ͼ ������ �ؽ�Ʈ ����
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
