using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Player
{
    public AchievementData[] Achievements;
    public List<LevelHistoric> Levels;

    public LevelHistoric GetLevel(Level.EType levelTypeId) 
    {
        LevelHistoric levelFinded = Levels.Find(level => level.TypeId == levelTypeId);
        levelFinded.TypeId = levelTypeId; // Dans le cas qu'il n'est pas trouvé

        return levelFinded;
        
    }

    public void SetLevel(LevelHistoric newLevel)
    {
        for(int i = 0; i < Levels.Count; i++)
        {
            LevelHistoric level = Levels[i];
            if (level.TypeId == newLevel.TypeId)
            {
                Levels[i] = newLevel;
                return;
            }
        }

        Levels.Add(newLevel);
    }
}
