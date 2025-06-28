using UnityEngine;
using UnityEngine.UI;

public class FlickeringLight : MonoBehaviour
{
    public float minAlpha = 0.2f;       // ���� ��ο� ����
    public float maxAlpha = 0.8f;       // ���� ���� ����
    public float flickerSpeed = 1.5f;   // �����̴� �ӵ� (���� Ŭ���� ������)

    private Image image;
    private float timeOffset;

    void Start()
    {
        image = GetComponent<Image>();
        timeOffset = Random.Range(0f, 10f); // ���� ���� ����
    }

    void Update()
    {
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(Time.time * flickerSpeed + timeOffset, 1));
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
