using UnityEngine;
using UnityEngine.UI;

public class FlickeringLight : MonoBehaviour
{
    public float minAlpha = 0.2f;       // 가장 어두운 상태
    public float maxAlpha = 0.8f;       // 가장 밝은 상태
    public float flickerSpeed = 1.5f;   // 깜빡이는 속도 (값이 클수록 빨라짐)

    private Image image;
    private float timeOffset;

    void Start()
    {
        image = GetComponent<Image>();
        timeOffset = Random.Range(0f, 10f); // 랜덤 시작 지점
    }

    void Update()
    {
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(Time.time * flickerSpeed + timeOffset, 1));
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
