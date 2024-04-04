using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDLevelGameTitle : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_LevelTitle;
    [SerializeField] LevelGameManager m_Manager;

    private void Start()
    {
        SubscribeAllActions();
    }

    private void SubscribeAllActions()
    {
        m_Manager.OnLoadLevelInfos += OnLoadLevelInfos;
    }

    private void OnLoadLevelInfos(Level level)
    {
        m_LevelTitle.text = level.Title;
    }
}
