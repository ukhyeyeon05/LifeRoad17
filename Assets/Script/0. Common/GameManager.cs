/*using UnityEngine;

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
}*/
/*using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
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

    public bool isGameOver = false;

    public void ChangeStatus(int moneyChange, int healthChange)
    {
        // Updated to use the recommended method
        PlayerStatus player = Object.FindFirstObjectByType<PlayerStatus>();
        if (player != null)
        {
            player.ApplyEffect(moneyChange, healthChange);

            if (player.money <= 0 || player.health <= 0)
            {
                isGameOver = true;
            }
        }
        else
        {
            Debug.LogError("PlayerStatus object not found in the scene.");
        }
    }
}
*/

/*using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
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

    public bool isGameOver = false;

    public void ChangeStatus(int moneyChange, int healthChange)
    {
        // Updated to use the recommended method
        PlayerStatus player = Object.FindFirstObjectByType<PlayerStatus>();
        if (player != null)
        {
            player.ApplyEffect(moneyChange, healthChange);

            if (player.money <= 0 || player.health <= 0)
            {
                isGameOver = true;
                Debug.Log("핵심 요소가 0이 되었으므로 베드 엔딩 진입");
                UnityEngine.SceneManagement.SceneManager.LoadScene("BedEnding1");
            }
        }
        else
        {
            Debug.LogError("PlayerStatus object not found in the scene.");
        }
    }
}*/
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
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

    public void ChangeStatus(int moneyChange, int healthChange)
    {
        PlayerStatus player = Object.FindFirstObjectByType<PlayerStatus>();
        if (player != null)
        {
            player.ApplyEffect(moneyChange, healthChange);

            if (player.money <= 0 || player.health <= 0)
            {
                Debug.Log("핵심 요소 중 0이 되어 베드 엔딩으로 전환");
                SceneManager.LoadScene("BedEnding1");
            }
        }
    }

    public void ResetStatusAndLoadScene(string sceneName)
    {
        PlayerStatus player = Object.FindFirstObjectByType<PlayerStatus>();
        if (player != null)
            player.ResetStatus();

        SceneManager.LoadScene(sceneName);
    }
}
