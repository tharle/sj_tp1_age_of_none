using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGameManager : MonoBehaviour
{
    [SerializeField] private Level.EType m_LevelType;
    [SerializeField] private LevelData m_LevelData; // scripted object


    private Level m_Level;

    private PlayerController m_PlayerController;

    void Start()
    {
        m_PlayerController = PlayerController.Instance;
    }

    public void PlayerGotEndOfLevel()
    {
        LevelHistoric newLevelGameData = ProcessLevelProgress();

        LevelHistoric oldLevelGameData = new LevelHistoric();
        SaveSystem.Load(data => oldLevelGameData = data.PlayerData.GetLevel(m_LevelType));

        //TODO: Aficher le resultat dans l'écran
        bool IsNewRecord = oldLevelGameData.RankId < newLevelGameData.RankId;
        if (IsNewRecord)
        {
            SaveSystem.Save(newLevelGameData);
        }

        // TODO
        Debug.Log($"GOT THE END OF LEVEL {m_LevelType}");
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
