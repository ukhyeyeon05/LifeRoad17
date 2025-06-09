using UnityEngine;
using TMPro;

public class FloatingTextMover : MonoBehaviour
{
    private float duration = 1.2f; // ��ü ���� �ð�
    private float speed = 40f;     // ���� �̵� �ӵ�
    private float fadeTime = 0.6f; // ������ ���̵� �ð�

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

            // ���� �ε巴�� �̵�
            transform.position = startPos + Vector3.up * speed * t;

            // ������ �κп����� ���� ����
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
