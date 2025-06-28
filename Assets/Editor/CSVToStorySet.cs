/*using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class CSVToStorySet
{
    [MenuItem("Tools/Import story_puz1.csv to StorySet")]
    public static void ImportCSV()
    {
        string csvPath = Application.dataPath + "/story_csv/story_puz1.csv";
        if (!File.Exists(csvPath))
        {
            Debug.LogError("CSV 파일을 찾을 수 없습니다: " + csvPath);
            return;
        }

        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length < 2)
        {
            Debug.LogError("CSV 내용이 부족합니다.");
            return;
        }

        List<StoryData> storyList = new List<StoryData>();

        for (int i = 1; i < lines.Length; i++) // 첫 줄은 헤더
        {
            string[] cols = SmartSplit(lines[i]);

            if (cols.Length < 13)
            {
                Debug.LogWarning($"[{i}행] 항목 수 부족: {cols.Length}개");
                continue;
            }

            StoryData data = new StoryData
            {
                storyText = cols[0],
                choices = new string[] { cols[1], cols[2], cols[3] },
                resultTexts = new string[] { cols[4], cols[5], cols[6] },
                moneyChanges = new int[]
                {
                    ParseSafe(cols[7]), ParseSafe(cols[8]), ParseSafe(cols[9])
                },
                healthChanges = new int[]
                {
                    ParseSafe(cols[10]), ParseSafe(cols[11]), ParseSafe(cols[12])
                }
            };

            storyList.Add(data);
        }

        StorySet storySet = ScriptableObject.CreateInstance<StorySet>();
        storySet.stories = storyList.ToArray(); 

        string assetPath = "Assets/Story/Puz1_StorySet.asset";
        AssetDatabase.CreateAsset(storySet, assetPath);
        AssetDatabase.SaveAssets();

        Debug.Log($"CSV → StorySet 생성 완료! 총 {storyList.Count}개 항목");
    }

    private static int ParseSafe(string s)
    {
        s = s.Trim();
        if (int.TryParse(s, out int result))
            return result;

        Debug.LogWarning($"숫자 변환 실패: '{s}' → 0으로 처리됨");
        return 0;
    }

    private static string[] SmartSplit(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string current = "";

        foreach (char c in line)
        {
            if (c == '"') inQuotes = !inQuotes;
            else if (c == ',' && !inQuotes)
            {
                result.Add(current.Trim());
                current = "";
            }
            else
            {
                current += c;
            }
        }

        result.Add(current.Trim());
        return result.ToArray();
    }
}*/
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class CSVToStorySet
{
    [MenuItem("Tools/Import ALL story_puzN.csv to StorySet")]
    public static void ImportAllCSVs()
    {
        string csvFolder = Application.dataPath + "/story_csv/";
        string[] csvFiles = Directory.GetFiles(csvFolder, "story_puz*.csv");

        foreach (string csvPath in csvFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(csvPath); // e.g. story_puz1
            string puzNumber = fileName.Replace("story_puz", "");        // e.g. "1"
            string assetPath = $"Assets/Story/Puz{puzNumber}_StorySet.asset";

            CreateStorySetFromCSV(csvPath, assetPath);
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"모든 CSV를 StorySet으로 변환 완료! ({csvFiles.Length}개 처리됨)");
    }

    private static void CreateStorySetFromCSV(string csvPath, string assetPath)
    {
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length < 2)
        {
            Debug.LogWarning($"{csvPath} 내용 부족. 건너뜀");
            return;
        }

        List<StoryData> storyList = new List<StoryData>();

        for (int i = 1; i < lines.Length; i++) // 헤더 스킵
        {
            string[] cols = SmartSplit(lines[i]);

            if (cols.Length < 13)
            {
                Debug.LogWarning($"[{Path.GetFileName(csvPath)}] {i}행 항목 수 부족: {cols.Length}개");
                continue;
            }

            StoryData data = new StoryData
            {
                storyText = cols[0],
                choices = new string[] { cols[1], cols[2], cols[3] },
                resultTexts = new string[] { cols[4], cols[5], cols[6] },
                moneyChanges = new int[]
                {
                    ParseSafe(cols[7]), ParseSafe(cols[8]), ParseSafe(cols[9])
                },
                healthChanges = new int[]
                {
                    ParseSafe(cols[10]), ParseSafe(cols[11]), ParseSafe(cols[12])
                }
            };

            storyList.Add(data);
        }

        StorySet storySet = ScriptableObject.CreateInstance<StorySet>();
        storySet.stories = storyList.ToArray();

        AssetDatabase.CreateAsset(storySet, assetPath);
        Debug.Log($"{Path.GetFileName(csvPath)} → {assetPath} 변환 완료 ({storyList.Count}개)");
    }

    private static int ParseSafe(string s)
    {
        s = s.Trim();
        if (int.TryParse(s, out int result))
            return result;

        Debug.LogWarning($"숫자 변환 실패: '{s}' → 0으로 처리됨");
        return 0;
    }

    private static string[] SmartSplit(string line)
    {
        List<string> result = new List<string>();
        bool inQuotes = false;
        string current = "";

        foreach (char c in line)
        {
            if (c == '"') inQuotes = !inQuotes;
            else if (c == ',' && !inQuotes)
            {
                result.Add(current.Trim());
                current = "";
            }
            else
            {
                current += c;
            }
        }

        result.Add(current.Trim());
        return result.ToArray();
    }
}
