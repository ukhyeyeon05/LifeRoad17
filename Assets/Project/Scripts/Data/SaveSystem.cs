using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public string puzzleId;
    public int currentIndex;
    public int money;
    public int health;
}

public static class SaveSystem
{
    private static string jsonPath => Application.persistentDataPath + "/save.json";

    public static void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(jsonPath, json);

        PlayerPrefs.SetInt("HasSaved", 1);
        PlayerPrefs.SetString("LastPuzzle", data.puzzleId);
        PlayerPrefs.Save();

        Debug.Log($"[SaveSystem] 저장 완료: {json}");
    }

    public static SaveData LoadGame()
    {
        if (!File.Exists(jsonPath)) return null;

        string json = File.ReadAllText(jsonPath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log($"[SaveSystem] 불러오기 성공: {json}");
        return data;
    }

    public static bool HasSavedGame()
    {
        return PlayerPrefs.GetInt("HasSaved", 0) == 1 && File.Exists(jsonPath);
    }

    public static void ClearSavedGame()
    {
        if (File.Exists(jsonPath))
            File.Delete(jsonPath);

        PlayerPrefs.DeleteKey("HasSaved");
        PlayerPrefs.DeleteKey("LastPuzzle");
        PlayerPrefs.Save();
    }
}