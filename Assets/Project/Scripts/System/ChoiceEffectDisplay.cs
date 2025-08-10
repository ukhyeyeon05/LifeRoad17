/*using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ChoiceEffectDisplay : MonoBehaviour
{
    [Header("StorySet 데이터 (Inspector에 드래그)")]
    public StorySet storySet;

    [Header("각 선택지별 Effect UI 부모 (3개)")]
    public Transform[] effectContainers; // 선택지별 위치 (예: HorizontalLayoutGroup)

    [Header("수치 UI 프리팹 (아이콘 + 수치 텍스트)")]
    public GameObject statusEffectItemPrefab;

    [Header("아이콘 매핑 (Key에 따라 이미지 지정)")]
    public List<IconMapping> globalMappings;

    void Start()
    {
        if (storySet == null || storySet.stories.Count == 0)
        {
            Debug.LogWarning("[ChoiceEffectDisplay] StorySet이 비어있습니다.");
            return;
        }

        DisplayEffectsForCurrentQuestion();
    }

    void DisplayEffectsForCurrentQuestion()
    {
        int currentIndex = 0; // 현재 문항 인덱스 (원하면 외부에서 설정 가능하게 수정 가능)
        StoryData data = storySet.stories[currentIndex];

        for (int i = 0; i < 3; i++)
        {
            ClearChildren(effectContainers[i]);

            ChoiceEffect choice = data.choiceEffects[i];
            foreach (StatusEffect eff in choice.effects)
            {
                if (string.IsNullOrEmpty(eff.key) || eff.value == 0) continue;

                GameObject item = Instantiate(statusEffectItemPrefab, effectContainers[i]);
                Image icon = item.transform.Find("Icon").GetComponent<Image>();
                TMP_Text valueText = item.transform.Find("Value").GetComponent<TMP_Text>();

                icon.sprite = GetIconByKey(eff.key);
                valueText.text = (eff.value > 0 ? "+" : "") + eff.value.ToString();
            }
        }
    }

    void ClearChildren(Transform parent)
    {
        foreach (Transform child in parent)
            Destroy(child.gameObject);
    }

    Sprite GetIconByKey(string key)
    {
        foreach (IconMapping map in globalMappings)
        {
            if (map.key == key)
                return map.icon;
        }
        return null;
    }
}*/
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class ChoiceEffectDisplay : MonoBehaviour
{
    [Header("선택지 3개 효과 표시용")]
    public Transform[] effectContainers; // 선택지별 효과가 들어갈 부모 오브젝트
    public GameObject statusEffectItemPrefab; // 프리팹: 아이콘 + 수치 조합

    [Header("퍼즐별 아이콘 설정")]
    public List<IconMapping> globalMappings; // 전체 공통 아이콘 매핑
    public StorySet storySet;

    public void UpdateEffectUI(int storyIndex)
    {
        if (storySet == null || storySet.stories == null || storyIndex >= storySet.stories.Count)
        {
            Debug.LogWarning("[ChoiceEffectDisplay] StorySet이 비어있거나 인덱스 오류입니다.");
            return;
        }

        // 기존 아이템 모두 제거
        foreach (Transform container in effectContainers)
        {
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }
        }

        // 현재 스토리 데이터의 효과 표시
        var story = storySet.stories[storyIndex];
        for (int i = 0; i < story.choiceEffects.Length; i++)
        {
            foreach (var effect in story.choiceEffects[i].effects)
            {
                var item = Instantiate(statusEffectItemPrefab, effectContainers[i]);
                var icon = item.transform.Find("Icon").GetComponent<Image>();
                var value = item.transform.Find("Value").GetComponent<TextMeshProUGUI>();

                var mapping = globalMappings.Find(m => m.key == effect.key);
                icon.sprite = mapping != null ? mapping.icon : null;
                value.text = (effect.value > 0 ? "+" : "") + effect.value.ToString();
            }
        }
    }
}
