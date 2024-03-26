using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AchivementCondition
{
    public EAchievementFlag AchievementFlagId;
    public int Value;
    public bool Unlocked;

    public void SetUnlocked(bool unlocked)
    {
        Unlocked = unlocked;
    }
}

