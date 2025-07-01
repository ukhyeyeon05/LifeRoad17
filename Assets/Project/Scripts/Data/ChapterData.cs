using UnityEngine;

public enum PuzzleStatus { NotStarted, Cleared_Normal, Cleared_Bad }

[System.Serializable]
public class ChapterData
{
    public string puzzleId;     // ��: "Puz01"
    public string sceneName;    // ��: "Puz1_Starting"
    public PuzzleStatus status; // �ʱⰪ: NotStarted
}
