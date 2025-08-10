using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CSVToStorySet : MonoBehaviour
{
    [MenuItem("Tools/Import story CSVs")]
    public static void ImportAll()
    {
        string folder = Application.dataPath + "/Project/Story/";
        string[] files = Directory.GetFiles(folder, "story_puz*.csv");

        foreach (string path in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(path);
            string assetPath = $"Assets/Project/Story/{fileName}_StorySet.asset";
            CreateStorySet(path, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("모든 CSV → StorySet 변환 완료");
    }

    private static void CreateStorySet(string csvPath, string assetPath)
    {
        var lines = File.ReadAllLines(csvPath);
        if (lines.Length <= 1)
        {
            Debug.LogWarning($"{Path.GetFileName(csvPath)}: 내용이 없음");
            return;
        }

        var storySet = ScriptableObject.CreateInstance<StorySet>();
        int addedCount = 0;

        for (int i = 1; i < lines.Length; i++) // 첫 줄은 헤더
        {
            var cols = SplitCSVLine(lines[i]);

            // 전체 Trim & 따옴표 제거
            for (int k = 0; k < cols.Length; k++)
            {
                cols[k] = cols[k].Trim().Replace("\"", "");
            }

            if (cols.Length < 13)
            {
                Debug.LogWarning($"줄 {i + 1} 열 부족: {cols.Length}");
                continue;
            }

            var data = new StoryData
            {
                question = cols[0],
                choices = new string[] { cols[1], cols[2], cols[3] },
                results = new string[] { cols[4], cols[5], cols[6] },
                choiceEffects = new ChoiceEffect[3]
            };

            for (int j = 0; j < 3; j++)
            {
                var effectList = new List<StatusEffect>();

                int moneyIndex = 7 + j;
                int healthIndex = 10 + j;

                if (moneyIndex < cols.Length &&
                    int.TryParse(cols[moneyIndex], out int money) &&
                    money != 0)
                {
                    effectList.Add(new StatusEffect { key = "돈", value = money });
                }

                if (healthIndex < cols.Length &&
                    int.TryParse(cols[healthIndex], out int health) &&
                    health != 0)
                {
                    effectList.Add(new StatusEffect { key = "건강", value = health });
                }

                data.choiceEffects[j] = new ChoiceEffect { effects = effectList.ToArray() };
            }

            storySet.stories.Add(data);
            addedCount++;
        }

        AssetDatabase.CreateAsset(storySet, assetPath);
        Debug.Log($"{Path.GetFileName(assetPath)} 생성됨 - 항목 수: {addedCount}");
    }

    private static string[] SplitCSVLine(string line)
    {
        return Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
    }
}
