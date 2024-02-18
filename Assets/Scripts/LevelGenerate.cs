using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    [SerializeField] private Transform m_StartingLevel;
    [SerializeField] private float m_DistanceSpawn = 2;
    private int m_IndexLastLevelSpawned = 0;

    private Transform m_LevelEndPoint;
    private Transform m_LastCreatedLevel;

    // Start is called before the first frame update
    private void Start()
    {
        m_LastCreatedLevel = m_StartingLevel;
    }

    private void Update()
    {   
        if (GetDistanceBetweenPlayerAndLastCreatedLevel() <= m_DistanceSpawn)
        {
            CreateLevelSelect();
        }
    }

    private float GetDistanceBetweenPlayerAndLastCreatedLevel() 
    {
        Transform playerTransform = PlayerMenuController.GetInstance().transform;
        float distance = Vector3.Distance(playerTransform.position, m_LastCreatedLevel.position);
        return distance;
    }

    private void CreateLevelSelect()
    {
        if(m_IndexLastLevelSpawned < PlayerMenuController.GetInstance().CountLevels())
        {
            m_LevelEndPoint = m_LastCreatedLevel.Find(GameParameters.PlataformName.END);
            m_LastCreatedLevel = SpawPlataform(m_LevelEndPoint.position);
            m_IndexLastLevelSpawned++;

            ChangeIndexLevelToPlataform();
        }
    }

    private void ChangeIndexLevelToPlataform() 
    {
        Transform transformTemp = m_LastCreatedLevel.Find(GameParameters.PlataformName.SPOT);
        Spot spot = transformTemp.GetComponent<Spot>();

        if (spot != null) 
        { 
            spot.SetIndexLevel(m_IndexLastLevelSpawned);
        }
    }

    private Transform SpawPlataform(Vector3 spawnPosition)
    {
        GameObject levelSelectBase = GetRandomPlataform();

        spawnPosition.x += GetOffsetWidthLevel(levelSelectBase.transform);
        GameObject newLevel = Instantiate(levelSelectBase, spawnPosition, Quaternion.identity);
        Vector3 newlevelPostion = newLevel.transform.position;
        newlevelPostion.y = m_LastCreatedLevel.transform.position.y; // prends la meme position du LastCreatedLevel
        newLevel.transform.position  = newlevelPostion;

        return newLevel.transform;
    }

    private float GetOffsetWidthLevel(Transform transformLevel)
    {
        Transform transformLevelEnd = transformLevel.Find(GameParameters.PlataformName.END);
        return transformLevelEnd.position.x;
    }

    private GameObject GetRandomPlataform()
    {
        return PlayerMenuController.GetInstance().GetLevelPrefabAt(m_IndexLastLevelSpawned);
    }
}
