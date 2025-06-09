using UnityEngine;
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
}
