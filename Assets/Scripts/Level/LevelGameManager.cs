using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGameManager : MonoBehaviour
{
    [SerializeField] private Level.EType m_LevelType;
    [SerializeField] private LevelData m_LevelData; // scripted object

    AchivementSystem m_AchivementSystem;


    private Level m_Level;

    private PlayerController m_PlayerController;

    void Start()
    {
        m_PlayerController = PlayerController.Instance;
        m_AchivementSystem = AchivementSystem.Instance;
    }

    public void PlayerGotEndOfLevel()
    {
        Time.timeScale = 0f;
        LevelHistoric newLevelGameData = ProcessLevelProgress();

        LevelHistoric oldLevelGameData = new LevelHistoric();
        SaveSystem.Load(data => oldLevelGameData = data.PlayerData.GetLevel(m_LevelType));

        bool IsNewRecord = oldLevelGameData.RankId < newLevelGameData.RankId;
        if (IsNewRecord)
        {
            SaveSystem.Save(newLevelGameData);
        }

        m_AchivementSystem.AddProgress(ToAchievementFlagId());
        Debug.Log($"GOT THE END OF LEVEL {m_LevelType}");
    }

    private EAchievementFlag ToAchievementFlagId()
    {
        EAchievementFlag achievementFlagId = EAchievementFlag.FinishedLevel1;
        switch (m_LevelType)
        {
            case Level.EType.Level1:
                achievementFlagId = EAchievementFlag.FinishedLevel1;
                break;
            case Level.EType.Level2:
                achievementFlagId = EAchievementFlag.FinishedLevel2;
                break;
            case Level.EType.Level3:
                achievementFlagId = EAchievementFlag.FinishedLevel2;
                break;
        }

        return achievementFlagId;
    }

    private LevelHistoric ProcessLevelProgress()
    {
        LevelHistoric levelGameData = new LevelHistoric();
        levelGameData.TypeId = m_LevelType;
        levelGameData.RankId = m_PlayerController.GetLevelRank();

        Debug.Log($"{m_LevelType} - {Enum.GetName(typeof(ERank), levelGameData.RankId)}");

        return levelGameData;
    }
}
