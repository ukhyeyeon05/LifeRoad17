using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 0;
    public int health = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ���� ���°� ���ӿ������� Ȯ��
    /// </summary>
    public bool isGameOver
    {
        get
        {
            return money <= 0 || health <= 0;
        }
    }

    /// <summary>
    /// �������� ���� ���� ��ġ�� ��ȭ��Ŵ
    /// </summary>
    public void ChangeStatus(int moneyDelta, int healthDelta)
    {
        money += moneyDelta;
        health += healthDelta;

        // ��ġ�� ������ �������� �ʵ��� �ϰ� �ʹٸ� �Ʒ� �ּ� ����
        // money = Mathf.Max(0, money);
        // health = Mathf.Max(0, health);

        Debug.Log($"[Status] money: {money}, health: {health}");
    }
}
