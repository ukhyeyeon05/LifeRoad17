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
    /// 현재 상태가 게임오버인지 확인
    /// </summary>
    public bool isGameOver
    {
        get
        {
            return money <= 0 || health <= 0;
        }
    }

    /// <summary>
    /// 선택지에 따라 상태 수치를 변화시킴
    /// </summary>
    public void ChangeStatus(int moneyDelta, int healthDelta)
    {
        money += moneyDelta;
        health += healthDelta;

        // 수치가 음수로 내려가지 않도록 하고 싶다면 아래 주석 해제
        // money = Mathf.Max(0, money);
        // health = Mathf.Max(0, health);

        Debug.Log($"[Status] money: {money}, health: {health}");
    }
}
