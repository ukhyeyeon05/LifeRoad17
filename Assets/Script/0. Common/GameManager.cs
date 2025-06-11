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
                Debug.Log("�ٽ� ��Ұ� 0�� �Ǿ����Ƿ� ���� ���� ����");
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
                Debug.Log("�ٽ� ��� �� 0�� �Ǿ� ���� �������� ��ȯ");
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
