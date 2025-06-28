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
            Debug.LogError("CSV ������ ã�� �� �����ϴ�: " + csvPath);
            return;
        }

        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length < 2)
        {
            Debug.LogError("CSV ������ �����մϴ�.");
            return;
        }

        List<StoryData> storyList = new List<StoryData>();

        for (int i = 1; i < lines.Length; i++) // ù ���� ���
        {
            string[] cols = SmartSplit(lines[i]);

            if (cols.Length < 13)
            {
                Debug.LogWarning($"[{i}��] �׸� �� ����: {cols.Length}��");
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

        Debug.Log($"CSV �� StorySet ���� �Ϸ�! �� {storyList.Count}�� �׸�");
    }

    private static int ParseSafe(string s)
    {
        s = s.Trim();
        if (int.TryParse(s, out int result))
            return result;

        Debug.LogWarning($"���� ��ȯ ����: '{s}' �� 0���� ó����");
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
        Debug.Log($"��� CSV�� StorySet���� ��ȯ �Ϸ�! ({csvFiles.Length}�� ó����)");
    }

    private static void CreateStorySetFromCSV(string csvPath, string assetPath)
    {
        string[] lines = File.ReadAllLines(csvPath);
        if (lines.Length < 2)
        {
            Debug.LogWarning($"{csvPath} ���� ����. �ǳʶ�");
            return;
        }

        List<StoryData> storyList = new List<StoryData>();

        for (int i = 1; i < lines.Length; i++) // ��� ��ŵ
        {
            string[] cols = SmartSplit(lines[i]);

            if (cols.Length < 13)
            {
                Debug.LogWarning($"[{Path.GetFileName(csvPath)}] {i}�� �׸� �� ����: {cols.Length}��");
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
        Debug.Log($"{Path.GetFileName(csvPath)} �� {assetPath} ��ȯ �Ϸ� ({storyList.Count}��)");
    }

    private static int ParseSafe(string s)
    {
        s = s.Trim();
        if (int.TryParse(s, out int result))
            return result;

        Debug.LogWarning($"���� ��ȯ ����: '{s}' �� 0���� ó����");
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
