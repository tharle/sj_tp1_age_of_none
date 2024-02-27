using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EMenuState
{
    MAIN_MENU,
    LEVEL_SELECT,
    LAST_LEVEL
}
public class MenuStateManager
{
    private static MenuStateManager m_Instance;

    private EMenuState m_CurrentStateId;

    public static MenuStateManager GetInstance()
    {
        if (m_Instance == null)
        {
            m_Instance = new MenuStateManager();
        }

        return m_Instance;
    }

    public MenuStateManager() {
        m_CurrentStateId = EMenuState.MAIN_MENU;
    }

    public void SetCurrentState(EMenuState menuStateId)
    {
        m_CurrentStateId = menuStateId;
    }

    public bool IsMainMenu()
    {
        return m_CurrentStateId == EMenuState.MAIN_MENU;
    }

    public bool IsLastLevel() 
    {
        return m_CurrentStateId == EMenuState.LAST_LEVEL;
    }
}
