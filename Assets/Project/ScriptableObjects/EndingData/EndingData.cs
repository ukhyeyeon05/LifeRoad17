using UnityEngine;

[CreateAssetMenu(fileName = "EndingData", menuName = "ScriptableObjects/EndingData", order = 1)]
public class EndingData : ScriptableObject
{
    public string puzzleId;

    public string normalTitle;
    public string normalFeedback;

    public string badTitle;
    public string badFeedback;

    public string realWorldInfo;
}