using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AchievementData
{
    public Sprite Icon;
    public string Title;
    public string Description;
    public bool Unlocked;

    public List<AchivementCondition> Conditions;

    public bool VerifyAndUnlock(AchivementSystem system)
    {
        Unlocked = true;
        for (int i = 0; i < Conditions.Count; i++ )
        {
            AchivementCondition condition = Conditions[i];
            if (condition.Unlocked) continue;

            int flagValue = system.GetArchivementFlagValue(condition.AchievementFlagId);

            Unlocked = flagValue >= condition.Value;
            condition.SetUnlocked(Unlocked);
            Conditions[i] = condition;

            if (!Unlocked) return false;
        }

        return Unlocked;
    }
}
