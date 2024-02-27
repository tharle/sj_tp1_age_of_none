using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelMenuController : MonoBehaviour
{
    [SerializeField] public LevelData m_Levels;
    private int m_CurrentLevelId; // 0 is menu, 1...N is Levels

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
    }

    private void OnStopInMainMenu()
    {
        MenuStateManager.GetInstance().SetCurrentState(EMenuState.MAIN_MENU);

        // TODO Call animation
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

    public LevelData.Level GetLevelAt(int indexLevel)
    {
        return m_Levels.levels[indexLevel];
    }

    public GameObject GetLevelObjetAt(int indexLevel)
    {
        LevelData.Level level = GetLevelAt(indexLevel);

        return Resources.Load<GameObject>("Prefabs/Levels/" + level.NamePrefab);
    }
}
