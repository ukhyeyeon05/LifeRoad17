using UnityEngine;

public class StartingUIController : MonoBehaviour
{
    public GameObject introPanel;      // �ٽ� ��� ���� �г�
    public GameObject choicePanel;     // ������ �г�
    public GameObject clickGuide;      // �հ��� Ŭ�� ���̵� �̹���

    private bool hasClicked = false;

    void Start()
    {
        introPanel.SetActive(true);
        choicePanel.SetActive(false);
        clickGuide.SetActive(true);    // Ŭ�� ���̵�� Intro���� ����
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
        clickGuide.SetActive(false);   // Ŭ�� ���̵� ����
    }
}
