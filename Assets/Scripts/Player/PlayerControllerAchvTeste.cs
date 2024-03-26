using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAchvTeste : MonoBehaviour
{
    //Variables membres
    private Player m_Player;

    // Externs
    private AchivementSystem m_AchivementSystem;

    private void Start()
    {
        m_AchivementSystem = AchivementSystem.Instance;

        InitData();
        SubscribeAll();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            m_AchivementSystem.AddProgress(EAchievementFlag.GoblinsKilledCount);
            Debug.Log("YOU KILL A GOBLIN!");
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            m_AchivementSystem.AddProgress(EAchievementFlag.PlayerDeathCount);
            Debug.Log("YOU DIE!");
        }
    }

    private void OnDestroy()
    {
        DesubscribeAll();
    }

    private void SubscribeAll()
    {
        m_AchivementSystem.OnAchivementChange += OnAchivementChange;
    }
    private void DesubscribeAll()
    {
        m_AchivementSystem.OnAchivementChange -= OnAchivementChange;
    }

    private void InitData()
    {
        SaveSystem.Load(LoadGame, NewGame);
    }

    private void LoadGame(SaveData data)
    {
        m_Player = data.PlayerData;
        m_AchivementSystem.Load(data.PlayerData.Achivements);
    }

    private void NewGame()
    {
        m_Player = new Player();
        AchivementData achData = Resources.Load<AchivementData>("Data/Archivements");
        m_AchivementSystem.Load(achData.Achivements);
    }

    private void OnAchivementChange(Achivement[] achivements, Dictionary<EAchievementFlag, int> m_AchivementFlagTracker)
    {
        // Save data
        m_Player.Achivements = achivements;
        SaveSystem.Save(m_Player);
    }
}
