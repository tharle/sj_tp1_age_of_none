using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_LevelsPrefabs;
    [SerializeField] private Transform m_StartingLevel;
    [SerializeField] private float m_DistanceSpawn = 2;
    private int m_LastLevelSpawned = 0;

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

        if(m_LastLevelSpawned < (m_LevelsPrefabs.Count - 1))
        {
            Debug.Log("SPAWN PLATAFORM");
            m_LevelEndPoint = m_LastCreatedLevel.Find(GameParameters.PlataformName.END);
            m_LastCreatedLevel = SpawPlataform(m_LevelEndPoint.position);

            m_LastLevelSpawned++;
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

        return m_LevelsPrefabs[m_LastLevelSpawned];
        
        /*switch (m_Controller.getCurrentSession())
        {
            case Session.SUMMER:
                int randomId = Random.Range(0, m_PlatformPrefabSummer.Count);
                return m_PlatformPrefabSummer[randomId];
            case Session.FALL:
                randomId = Random.Range(0, m_PlatformPrefabFall.Count);
                return m_PlatformPrefabFall[randomId];
            case Session.WINTER:
                randomId = Random.Range(0, m_PlatformPrefabWinter.Count);
                return m_PlatformPrefabWinter[randomId];
            case Session.SPRING:
            default:
                randomId = Random.Range(0, m_PlatformPrefabSpring.Count);
                return m_PlatformPrefabSpring[randomId];
        }*/
    }
}
