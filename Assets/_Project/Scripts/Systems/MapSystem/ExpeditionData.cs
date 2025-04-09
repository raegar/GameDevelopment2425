using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Unit
{
    public string unitName;
    public Sprite unitPortrait;
    public int unitStrength;
}

[Serializable]
public class Army
{
    public List<Unit> units;
    public int totalStrength => CalculateTotalStrength();

    private int CalculateTotalStrength()
    {
        int strength = 0;
        foreach (Unit unit in units)
        {
            strength += unit.unitStrength;
        }
        return strength;
    }
}

[Serializable]
public class Expedition
{
    public string expeditionName;
    public float duration; // in seconds
    public bool isOngoing;
    public DateTime endTime;
    public Reward reward;
    public int requiredStrength;
}

[Serializable]
public class Reward
{
    public string rewardType;
    public int amount;
}
