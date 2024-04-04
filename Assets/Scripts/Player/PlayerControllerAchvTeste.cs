using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelData;

public class PlayerControllerAchvTeste : MonoBehaviour
{
    //Variables membres
    private Player m_Player;

    // Externs
    private AchivementSystem m_AchivementSystem;
    private BundleLoader m_Loader;

    private void Start()
    {
        m_AchivementSystem = AchivementSystem.Instance;
        m_Loader = BundleLoader.Instance;

        InitData();
        SubscribeAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            m_AchivementSystem.AddProgress(EAchievementFlag.FinishedLevel1);
            Debug.Log("L1!");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            m_AchivementSystem.AddProgress(EAchievementFlag.FinishedLevel2);
            Debug.Log("L2!");
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            m_AchivementSystem.AddProgress(EAchievementFlag.FinishedLevel3);
            Debug.Log("L3!");
        }
    }

    private void OnDestroy()
    {
        DesubscribeAll();
    }

    private void SubscribeAll()
    {
        m_AchivementSystem.OnAchievementChange += OnAchivementChange;
    }
    private void DesubscribeAll()
    {
        m_AchivementSystem.OnAchievementChange -= OnAchivementChange;
    }

    private void InitData()
    {
        SaveSystem.Load(LoadGame, NewGame);
    }

    private void LoadGame(SaveData data)
    {
        m_Player = data.PlayerData;
        m_AchivementSystem.Load(data.PlayerData.Achievements);
    }

    private void NewGame()
    {
        m_Player = new Player();
        AchivementData achData = m_Loader.Load<AchivementData>(GameParameters.BundleNames.SCRIT_OBJETS, nameof(AchivementData));
        m_AchivementSystem.Load(achData.Achivements);
    }

    private void OnAchivementChange(AchievementData[] achivements)
    {
        // Save data
        m_Player.Achievements = achivements;
        SaveSystem.Save(m_Player);
    }
}
