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
        "빈곤층 감소와 사회안전망 강화", "식량안보 및 지속가능한 농업 강화",
        "건강하고 행복한 삶 보장", "모두를 위한 양질의 교육",
        "성평등 보장", "건강하고 안전한 물관리",
        "에너지의 친환경적 생산과 소비", "좋은 일자리 확대와 경제성장",
        "산업의 성장과 혁신 활성화 및 사회기반시설 구축", "모든 종류의 불평등 해소",
        "지속가능한 도시와 주거지 조성", "지속 가능한 생산과 소비",
        "기후변화와 대응", "해양생태계 보전",
        "육상생태계 보전", "평화 · 정의 · 포용",
        "지구촌 협력 강화"
    };

    void Start()
    {
        int puzzleID = PlayerPrefs.GetInt("선택된 주제", 0);
        if (puzzleID >= 0 && puzzleID < titles.Length)
            titleText.text = titles[puzzleID];
        else titleText.text = "알 수 없음";

        backButton.onClick.AddListener(() => SceneManager.LoadScene("0_ChapterSelect"));
        startButton.onClick.AddListener(() => SceneManager.LoadScene("2_ChoiceButton"));
    }
}