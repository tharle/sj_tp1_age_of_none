using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HUDManager : MonoBehaviour
{

    [SerializeField] GameObject m_MainMenuGameObject;
    [SerializeField] GameObject m_LevelMenuGameObject;
    [SerializeField] LevelMenuController m_LevelController;

    private void Start()
    {
        SubscribleAllActions();
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

        // add value Level dans les objets des level
    }
}
