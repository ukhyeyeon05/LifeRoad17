using UnityEngine;
using TMPro;

public class FloatingStatusText : MonoBehaviour
{
    [Header("애니메이션 설정")]
    public float floatSpeed = 50f;
    public float fadeDuration = 1f;

    [Header("텍스트 설정")]
    public float fontSize = 28f;
    public Color positiveColor = Color.white;
    public Color negativeColor = Color.red;

    private TextMeshProUGUI tmp;
    private RectTransform rect;
    private float lifetime;

    public static void Show(int amount, string type)
    {
        GameObject prefab = Resources.Load<GameObject>("FloatingStatusText");
        if (prefab == null)
        {
            Debug.LogError("FloatingStatusText 프리팹이 Resources 안에 없습니다.");
            return;
        }

        Transform target = GameObject.Find(type == "money" ? "MoneyText" : "HealthText")?.transform;
        if (target == null)
        {
            Debug.LogError($"{type}Text 오브젝트를 찾을 수 없습니다.");
            return;
        }

        GameObject instance = Instantiate(prefab);
        instance.transform.SetParent(target.parent, false);
        instance.transform.position = target.position + new Vector3(-195f, -50f, 0f);

        FloatingStatusText floating = instance.GetComponent<FloatingStatusText>();
        floating.Setup(amount);
    }

    void Awake()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        rect = GetComponent<RectTransform>();
    }

    public void Setup(int amount)
    {
        string sign = amount >= 0 ? "+" : "";
        tmp.text = sign + amount.ToString();
        tmp.fontSize = fontSize;
        tmp.color = amount >= 0 ? positiveColor : negativeColor;

        lifetime = fadeDuration;
    }

    void Update()
    {
        rect.anchoredPosition += Vector2.up * floatSpeed * Time.deltaTime;

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            // 점점 투명하게
            Color c = tmp.color;
            c.a = Mathf.Clamp01(lifetime / fadeDuration);
            tmp.color = c;
        }
    }
}
