using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapterInfo : MonoBehaviour
{
    public Text titleText;
    public Button backButton;
    public Button startButton;

    private string[] titles =
    {
        "����� ���ҿ� ��ȸ������ ��ȭ", "�ķ��Ⱥ� �� ���Ӱ����� ��� ��ȭ",
        "�ǰ��ϰ� �ູ�� �� ����", "��θ� ���� ������ ����",
        "����� ����", "�ǰ��ϰ� ������ ������",
        "�������� ģȯ���� ����� �Һ�", "���� ���ڸ� Ȯ��� ��������",
        "����� ����� ���� Ȱ��ȭ �� ��ȸ��ݽü� ����", "��� ������ ����� �ؼ�",
        "���Ӱ����� ���ÿ� �ְ��� ����", "���� ������ ����� �Һ�",
        "���ĺ�ȭ�� ����", "�ؾ���°� ����",
        "������°� ����", "��ȭ �� ���� �� ����",
        "������ ���� ��ȭ"
    };

    void Start()
    {
        int puzzleID = PlayerPrefs.GetInt("���õ� ����", 0);
        if (puzzleID >= 0 && puzzleID < titles.Length)
            titleText.text = titles[puzzleID];
        else titleText.text = "�� �� ����";

        backButton.onClick.AddListener(() => SceneManager.LoadScene("0_ChapterSelect"));
        startButton.onClick.AddListener(() => SceneManager.LoadScene("2_ChoiceButton"));
    }
}