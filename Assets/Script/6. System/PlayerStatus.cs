/*using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int money = 1;
    public int health = 1;

    public void ApplyEffect(int moneyChange, int healthChange)
    {
        money += moneyChange;
        health += healthChange;

        money = Mathf.Max(0, money);
        health = Mathf.Max(0, health);

        Debug.Log($"[PlayerStatus] money: {money}, health: {health}");
    }
}*/
/*using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int money = 1;
    public int health = 1;

    public void ApplyEffect(int moneyChange, int healthChange)
    {
        money += moneyChange;
        health += healthChange;

        money = Mathf.Max(0, money);
        health = Mathf.Max(0, health);

        Debug.Log($"[PlayerStatus] money: {money}, health: {health}");
    }
}*/
/*using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int money = 1;
    public int health = 1;

    public void ApplyEffect(int moneyChange, int healthChange)
    {
        money += moneyChange;
        health += healthChange;

        money = Mathf.Max(0, money);
        health = Mathf.Max(0, health);

        Debug.Log("[PlayerStatus] money: {money}, health: {health}");
    }
}
*/

using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public int money = 1;
    public int health = 1;

    public void ApplyEffect(int moneyChange, int healthChange)
    {
        money += moneyChange;
        health += healthChange;

        money = Mathf.Max(0, money);
        health = Mathf.Max(0, health);

        Debug.Log($"[PlayerStatus] money: {money}, health: {health}");
    }

    public void ResetStatus()
    {
        money = 1;
        health = 1;
        Debug.Log("«ŸΩ… ø‰º“ √ ±‚»≠µ ");
    }
}
