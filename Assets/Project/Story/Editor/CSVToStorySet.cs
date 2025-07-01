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
            Debug.LogWarning($" {Path.GetFileName(csvPath)}: 내용이 없음");
            return;
        }

        var storySet = ScriptableObject.CreateInstance<StorySet>();
        int addedCount = 0;

        for (int i = 1; i < lines.Length; i++) // 첫 줄은 헤더
        {
            var cols = SplitCSVLine(lines[i]);
            if (cols.Length < 13)
            {
                Debug.LogWarning($" 줄 {i + 1} 열 부족: {cols.Length}");
                continue;
            }

            var data = new StoryData
            {
                question = cols[0].Trim(),
                choices = new string[] { cols[1].Trim(), cols[2].Trim(), cols[3].Trim() },
                results = new string[] { cols[4].Trim(), cols[5].Trim(), cols[6].Trim() },
                choiceEffects = new ChoiceEffect[3]
            };

            for (int j = 0; j < 3; j++)
            {
                var effectList = new List<StatusEffect>();

                int moneyIndex = 7 + j;
                int healthIndex = 10 + j;

                if (moneyIndex < cols.Length &&
                    int.TryParse(cols[moneyIndex].Trim().Replace("\"", ""), out int money) &&
                    money != 0)
                {
                    effectList.Add(new StatusEffect { key = "돈", value = money });
                    Debug.Log($" [{i}] choice{j + 1} - 돈: {money}");
                }

                if (healthIndex < cols.Length &&
                    int.TryParse(cols[healthIndex].Trim().Replace("\"", ""), out int health) &&
                    health != 0)
                {
                    effectList.Add(new StatusEffect { key = "건강", value = health });
                    Debug.Log($" [{i}] choice{j + 1} - 건강: {health}");
                }

                data.choiceEffects[j] = new ChoiceEffect { effects = effectList.ToArray() };
            }

            storySet.storyList.Add(data);
            addedCount++;
        }

        AssetDatabase.CreateAsset(storySet, assetPath);
        Debug.Log($" {Path.GetFileName(assetPath)} 생성됨 - 항목 수: {addedCount}");
    }

    private static string[] SplitCSVLine(string line)
    {
        return Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
    }
}
