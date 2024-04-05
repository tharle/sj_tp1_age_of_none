using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_DangerPrefab;
    [SerializeField] private float m_Speed = 3f;
    [SerializeField] private float m_Delay = 0.5f;
    [SerializeField] private float m_LifeTime = 5f;
    [SerializeField] private EAudio m_AudioId = EAudio.SFXFireBall;

    private List<GameObject> m_DangerPool = new List<GameObject>();

    private PlayerController m_PlayerController;

    private void Start()
    {
        m_PlayerController = PlayerController.Instance;

        StartCoroutine(SpawnDangers());
    }

    private IEnumerator SpawnDangers()
    {
        while (m_PlayerController.IsPlaying)
        {
            GameObject danger = GetDangerAvaible();
            danger.transform.position = transform.position;
            danger.transform.forward = transform.forward;
            danger.GetComponent<DangerController>().Speed = m_Speed;
            danger.GetComponent<DangerController>().LifeTime = m_LifeTime;
            danger.GetComponent<DangerController>().AudioId = m_AudioId;

            yield return new WaitForSeconds(m_Delay);
        }
    }

    private GameObject GetDangerAvaible()
    {
        foreach (GameObject dangerObject in m_DangerPool)
        {
            if (!dangerObject.activeInHierarchy)
            {
                dangerObject.SetActive(true);
                return dangerObject;
            }
        }

        GameObject newDangerObject = Instantiate(m_DangerPrefab);
        m_DangerPool.Add(newDangerObject);
        return newDangerObject;
    }
}
