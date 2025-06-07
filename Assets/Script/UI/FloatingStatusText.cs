using UnityEngine;
using TMPro;

public class FloatingStatusText : MonoBehaviour
{
    public static void Show(int amount, string type)
    {
        GameObject prefab = Resources.Load<GameObject>("FloatingStatusText");
        if (prefab == null)
        {
            Debug.LogError("FloatingStatusText �������� Resources �ȿ� �����ϴ�.");
            return;
        }

        Transform target = GameObject.Find(type == "money" ? "MoneyText" : "HealthText")?.transform;
        if (target == null)
        {
            Debug.LogError($"{type}Text ������Ʈ�� ã�� �� �����ϴ�.");
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
