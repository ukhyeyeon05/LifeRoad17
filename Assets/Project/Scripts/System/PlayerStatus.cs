/*using UnityEngine;
using TMPro;

public class PlayerStatus : MonoBehaviour
{
    public int money = 0;
    public int health = 0;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI healthText;

    public void ApplyEffect(StatusEffect[] effects)
    {
        foreach (var effect in effects)
        {
            switch (effect.key.ToLower()) // key는 대소문자 구분하므로 소문자로 통일
            {
                case "money":
                case "돈":
                    money += effect.value;
                    break;
                    
                case "health":
                case "건강":
                    health += effect.value;
                    break;
            }
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (moneyText != null) moneyText.text = money.ToString();
        if (healthText != null) healthText.text = health.ToString();
    }
}*/
using TMPro;
using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    public int money = 0;
    public int health = 0;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI healthText;

    public TextMeshProUGUI changeText1; // ChangeText1에 연결
    public TextMeshProUGUI changeText2; // ChangeText2에 연결

    private Coroutine changeRoutine1;
    private Coroutine changeRoutine2;

    public static PlayerStatus Instance;

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


    public void ApplyEffect(StatusEffect[] effects)
    {
        foreach (var effect in effects)
        {
            switch (effect.key)
            {
                case "돈":
                    money += effect.value;
                    ShowChange(changeText1, ref changeRoutine1, effect.value);
                    break;
                case "건강":
                    health += effect.value;
                    ShowChange(changeText2, ref changeRoutine2, effect.value);
                    break;
            }
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (moneyText != null) moneyText.text = money.ToString();
        if (healthText != null) healthText.text = health.ToString();
    }

    private void ShowChange(TextMeshProUGUI text, ref Coroutine routine, int value)
    {
        if (routine != null)
            StopCoroutine(routine);
        routine = StartCoroutine(FadeChange(text, value));
    }

    private IEnumerator FadeChange(TextMeshProUGUI text, int value)
    {
        text.gameObject.SetActive(true);
        text.text = (value > 0 ? "+" : "") + value;

        text.color = value > 0 ? new Color(1f, 0.7f, 0.2f) : new Color(1f, 0.3f, 0.3f); // 노랑 / 빨강

        Vector3 start = text.rectTransform.localPosition;
        Vector3 end = start + new Vector3(0, 30f, 0);

        float duration = 1f;
        float t = 0f;

        CanvasGroup group = text.GetComponent<CanvasGroup>();
        if (group == null) group = text.gameObject.AddComponent<CanvasGroup>();
        group.alpha = 1f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float progress = t / duration;

            text.rectTransform.localPosition = Vector3.Lerp(start, end, progress);
            group.alpha = 1f - progress;

            yield return null;
        }

        text.gameObject.SetActive(false);
        text.rectTransform.localPosition = start;
        group.alpha = 1f;
    }

    public void ResetStatus()
    {
        money = 0;
        health = 0;
        UpdateUI();
    }

}
