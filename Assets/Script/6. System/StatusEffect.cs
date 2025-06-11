using UnityEngine;

[System.Serializable]
public class StatusEffect
{
    public int moneyChange;
    public int healthChange;
    public int cropChange;
    public int waterChange;
    public int mentalChange;
    public int wasteChange;

    // 확장 가능한 기타 요소들
    public int housingSafetyChange;
    public int pollutionChange;
    public int globalCooperationChange;
}
