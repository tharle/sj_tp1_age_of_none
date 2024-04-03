using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGameEnd : MonoBehaviour
{
    LevelGameManager m_Manager;
    private void Start()
    {
        m_Manager = GetComponentInParent<LevelGameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            m_Manager.PlayerGotEndOfLevel();
        }
    }
}
