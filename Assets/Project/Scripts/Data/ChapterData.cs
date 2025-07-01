using UnityEngine;

public enum PuzzleStatus { NotStarted, Cleared_Normal, Cleared_Bad }

[System.Serializable]
public class ChapterData
{
    public string puzzleId;     // 예: "Puz01"
    public string sceneName;    // 예: "Puz1_Starting"
    public PuzzleStatus status; // 초기값: NotStarted
}
