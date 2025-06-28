using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public int money = 1;
    public int health = 1;

    private bool hasEnded = false;

    public void ApplyEffect(int moneyChange, int healthChange)
    {
        money += moneyChange;
        health += healthChange;

        money = Mathf.Max(0, money);
        health = Mathf.Max(0, health);

        Debug.Log($"[PlayerStatus] money: {money}, health: {health}");

        CheckForBadEnding(); // ���� ��ȭ �� üũ
    }

    void CheckForBadEnding()
    {
        if (!hasEnded && (money <= 0 || health <= 0))
        {
            hasEnded = true;
            SceneManager.LoadScene("BadEnding1"); // ���� ���忣�� �� �̸� ���
        }
    }

    public void ResetStatus()
    {
        money = 0;
        health = 0;
        hasEnded = false;
        Debug.Log("���� �ʱ�ȭ��");
    }
}
