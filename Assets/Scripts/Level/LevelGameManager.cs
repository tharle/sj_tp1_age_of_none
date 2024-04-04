using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelGameManager : MonoBehaviour
{
    [SerializeField] private Level.EType m_LevelType;
    [SerializeField] private LevelData m_LevelData; // scripted object

    AchivementSystem m_AchivementSystem;


    private Level m_Level;

    private PlayerController m_PlayerController;
    private BundleLoader m_Loader;

    private AchievementData[] m_Achievements;

    void Start()
    {
        m_PlayerController = PlayerController.Instance;
        m_AchivementSystem = AchivementSystem.Instance;
        m_Loader = BundleLoader.Instance;
        SubscribeAllActions();
        LoadData();
    }

    private void SubscribeAllActions()
    {
        m_AchivementSystem.OnAchievementChange += OnAchievementChange;
    }

    private void OnAchievementChange(AchievementData[] achievements)
    {
        m_Achievements = achievements;
    }

    private void LoadData()
    {
        SaveSystem.Load(LoadAchievementData, NewAchievementData);
    }

    private void LoadAchievementData(SaveData data)
    {
        if (data.PlayerData.Achievements.Length > 0) m_AchivementSystem.Load(data.PlayerData.Achievements);
        else NewAchievementData();
    }


    // Dans le cas qui les Achievement ont été jamais enregistré
    private void NewAchievementData()
    {
        AchivementData achData = m_Loader.Load<AchivementData>(GameParameters.BundleNames.SCRIT_OBJETS, nameof(AchivementData));
        m_AchivementSystem.Load(achData.Achivements);
    }

    public void PlayerGotEndOfLevel()
    {
        LevelHistoric newLevelGameData = ProcessLevelProgress();

        LevelHistoric oldLevelGameData = new LevelHistoric();
        SaveSystem.Load(data => oldLevelGameData = data.PlayerData.GetLevel(m_LevelType));


        bool IsNewRecord = oldLevelGameData.RankId < newLevelGameData.RankId;
        if (IsNewRecord)
        {
            m_AchivementSystem.AddProgress(ToAchievementFlagId());
            SaveSystem.Save(newLevelGameData, m_Achievements);
        }

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
                achievementFlagId = EAchievementFlag.FinishedLevel3;
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
