using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ChapterMeta", menuName = "LifeRoad17/ChapterMeta")]
public class ChapterMeta : ScriptableObject
{
    public List<ChapterData> chapters = new List<ChapterData>();
}
