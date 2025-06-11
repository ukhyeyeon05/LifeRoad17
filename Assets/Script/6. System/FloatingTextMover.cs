using UnityEngine;
using TMPro;

public class FloatingTextMover : MonoBehaviour
{
    private float duration = 1.2f; // 전체 지속 시간
    private float speed = 40f;     // 위로 이동 속도
    private float fadeTime = 0.6f; // 마지막 페이드 시간

    private TextMeshProUGUI tmp;
    private Color originalColor;

    void Start()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        if (tmp != null)
        {
            originalColor = tmp.color;
        }

        StartCoroutine(FloatAndFade());
    }

    System.Collections.IEnumerator FloatAndFade()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            // 위로 부드럽게 이동
            transform.position = startPos + Vector3.up * speed * t;

            // 마지막 부분에서만 투명도 감소
            if (t > (1f - fadeTime) && tmp != null)
            {
                float fade = 1f - ((t - (1f - fadeTime)) / fadeTime);
                tmp.color = new Color(originalColor.r, originalColor.g, originalColor.b, fade);
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
