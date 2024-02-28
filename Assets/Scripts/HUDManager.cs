using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    [SerializeField] GameObject m_MainMenuGameObject;
    [SerializeField] GameObject m_LevelMenuGameObject;
    [SerializeField] LevelMenuController m_LevelController;

    // Element level
    [SerializeField] Image m_Rank;
    [SerializeField] TextMeshProUGUI m_Title;
    [SerializeField] TextMeshProUGUI m_Description;

    Dictionary<ERank, Sprite> m_RankStamps = new Dictionary<ERank, Sprite>();

    private void Start()
    {
        SubscribleAllActions();
        LoadAllRankStamps();
    }

    private void LoadAllRankStamps()
    {
        m_RankStamps = new Dictionary<ERank, Sprite>();

        m_RankStamps[ERank.S] = Resources.Load<Sprite>(GameParameters.Directory.RESOURCES_RANK+ "S");
        m_RankStamps[ERank.A] = Resources.Load<Sprite>(GameParameters.Directory.RESOURCES_RANK+ "A");
        m_RankStamps[ERank.B] = Resources.Load<Sprite>(GameParameters.Directory.RESOURCES_RANK+ "B");
        m_RankStamps[ERank.C] = Resources.Load<Sprite>(GameParameters.Directory.RESOURCES_RANK+ "C");

    }

    // ----------------------------------------
    // ACTIONS
    // ----------------------------------------

    private void SubscribleAllActions()
    {
        m_LevelController.SubscribeOnToggleMainMenu(OnToggleMainMenu);
        m_LevelController.SubscribeOnToggleLevelMenu(OnToggleLevelMenu);
    }

    public void OnToggleMainMenu(bool show)
    {
        m_MainMenuGameObject.SetActive(show);
    }

    public void OnToggleLevelMenu(bool show, LevelData.Level level)
    {
        m_LevelMenuGameObject.SetActive(show);

        if (!show) return;

        FillLevelInfos(level);
    }

    private void FillLevelInfos(LevelData.Level level) 
    {
        m_Title.text = level.Title;
        m_Description.text = level.Description;
        m_Rank.sprite = m_RankStamps[level.RankId];
    }
}
