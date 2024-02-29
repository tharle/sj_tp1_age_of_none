using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelMenuController : MonoBehaviour
{
    [SerializeField] public LevelData m_Levels;
    private int m_CurrentLevelId; // 0 is menu, 1...N is Levels
    
    private event Action<bool> m_OnToggleMainMenu;
    private event Action<bool, LevelData.Level> m_OnToggleLevelMenu;

    private void Start()
    {
        m_CurrentLevelId = -1;
        SubscribleAllActions();
    }

    // ----------------------------------------
    // ACTIONS
    // ----------------------------------------

    private void SubscribleAllActions()
    {
        PlayerMenuController.GetInstance().SubscribeOnChangeCurrentLevelId(OnChangeCurrentLevelId);
        PlayerMenuController.GetInstance().SubscribeOnMoveToNextSpot(OnMoveToNextSpot);
    }

    private void OnMoveToNextSpot()
    {
        if(MenuStateManager.GetInstance().IsMainMenu()) m_OnToggleMainMenu?.Invoke(false);
        else m_OnToggleLevelMenu?.Invoke(false, GetCurrentLevel());
    }

    private void OnChangeCurrentLevelId(int currentLevelId)
    {
        m_CurrentLevelId = currentLevelId;

        if (currentLevelId < 0) OnStopInMainMenu();
        else OnStopInLevel();
    }


    private void OnStopInLevel()
    {
        EMenuState menuStateId = IsLastLevel() ? EMenuState.LAST_LEVEL : EMenuState.LEVEL_SELECT;
        MenuStateManager.GetInstance().SetCurrentState(menuStateId);
        // TODO Call animation

        m_OnToggleLevelMenu?.Invoke(true, GetCurrentLevel());
    }

    private void OnStopInMainMenu()
    {
        MenuStateManager.GetInstance().SetCurrentState(EMenuState.MAIN_MENU);

        m_OnToggleMainMenu?.Invoke(true);
    }

    public void SubscribeOnToggleMainMenu(Action<bool> action)
    {
        m_OnToggleMainMenu = action;
    }

    public void SubscribeOnToggleLevelMenu(Action<bool, LevelData.Level> action)
    {
        m_OnToggleLevelMenu = action;
    }


    // ----------------------------------------
    // GETS AND SETS
    // ----------------------------------------

    public int GetCurrentLevelId()
    {
        return m_CurrentLevelId;
    }

    public bool IsLastLevel()
    {
        return m_CurrentLevelId >= CountLevels() - 1;
    }

    public int CountLevels()
    {
        return m_Levels.levels.Count;
    }

    public LevelData.Level GetCurrentLevel()
    {
        return GetLevelAt(m_CurrentLevelId);
    }

    public LevelData.Level GetLevelAt(int currentLevelId)
    {
        return m_Levels.levels[currentLevelId];
    }

    public GameObject GetLevelObjetAt(int currentLevelId)
    {
        LevelData.Level level = GetLevelAt(currentLevelId);

        return Resources.Load<GameObject>("Prefabs/Levels/" + level.NamePrefab);
    }

    // ----------------------------------------
    // MOUSE EVENTS
    // ----------------------------------------
    public void OnClickNextLevel()
    {
        PlayerMenuController.GetInstance().MoveRight();
        AudioManager.GetInstance().Play(EAudio.SFX_CONFIRM, Input.mousePosition);
    }

    public void OnClickPreviusLevel()
    {
        PlayerMenuController.GetInstance().MoveLeft();
        AudioManager.GetInstance().Play(EAudio.SFX_CONFIRM, Input.mousePosition);
    }

    public void OnClickQuit()
    {
        AudioManager.GetInstance().Play(EAudio.SFX_CONFIRM, Input.mousePosition);
        Application.Quit();
    }
}
