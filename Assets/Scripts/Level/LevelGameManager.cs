using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGameManager : MonoBehaviour
{
    //[SerializeField] public LevelData m_LevelData;
    [SerializeField] private Level.EType m_LevelType;

    private 

    void Start()
    {
        //m_level = m_LevelData.levels.Find(level => level.TypeId == m_LevelType);
    }

    public void PlayerGotEndOfLevel()
    {
        // TODO
    }
}
