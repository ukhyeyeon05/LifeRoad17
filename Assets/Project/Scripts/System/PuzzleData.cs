using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleData", menuName = "LifeRoad17/PuzzleData")]
public class PuzzleData : ScriptableObject
{
    public List<PuzzleEntry> entries;
}

[System.Serializable]
public class PuzzleEntry
{
    public string storyText;
    public string[] choices = new string[3];
    public string[] results = new string[3];
    public int[] moneyEffects = new int[3];
    public int[] healthEffects = new int[3];
}
