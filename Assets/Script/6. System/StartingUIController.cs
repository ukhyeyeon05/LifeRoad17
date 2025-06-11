using UnityEngine;

public class StartingUIController : MonoBehaviour
{
    public GameObject introPanel;      // 핵심 요소 설명 패널
    public GameObject choicePanel;     // 선택지 패널
    public GameObject clickGuide;      // 손가락 클릭 가이드 이미지

    private bool hasClicked = false;

    void Start()
    {
        introPanel.SetActive(true);
        choicePanel.SetActive(false);
        clickGuide.SetActive(true);    // 클릭 가이드는 Intro에만 보임
    }

    void Update()
    {
        if (!hasClicked && Input.GetMouseButtonDown(0))
        {
            ShowChoicePanel();
            hasClicked = true;
        }
    }

    void ShowChoicePanel()
    {
        introPanel.SetActive(false);
        choicePanel.SetActive(true);
        clickGuide.SetActive(false);   // 클릭 가이드 숨김
    }
}
