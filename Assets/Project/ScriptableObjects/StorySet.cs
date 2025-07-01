using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "StorySet", menuName = "Story/StorySet")]
public class StorySet : ScriptableObject
{
    public List<StoryData> storyList = new List<StoryData>();
}
