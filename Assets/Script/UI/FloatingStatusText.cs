using UnityEngine;
using TMPro;

public class FloatingStatusText : MonoBehaviour
{
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
        instance.transform.position = target.position + new Vector3(-110f, -40f, 0f);

        TextMeshProUGUI tmp = instance.GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = (amount >= 0 ? "+" : "") + amount.ToString();

        instance.AddComponent<FloatingTextMover>();
    }
}
