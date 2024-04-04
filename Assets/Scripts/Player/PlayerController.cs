using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player m_Player;


    private int m_CoinsCount;
    private int m_CoinsMax;
    private bool m_IsPlayerDied = false;
    private bool m_IsPlaying = true;
    public bool IsPlaying { get { return m_IsPlaying; } }

    public event Action<int> OnChangeCoinValue;

    public event Action<ERank> OnShowProgress;

    private static PlayerController m_Instance;
    public static PlayerController Instance { get { return m_Instance; } }

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
            Destroy(gameObject);

        m_Instance = this;
    }

    private void Start()
    {
        m_CoinsCount = 0;
        m_CoinsMax = FindObjectsOfType<CoinController>().Length;
    }

    public void CollectCoin(int value)
    {
        Debug.Log($"We collect {value} gold. ");
        m_CoinsCount += value;
        OnChangeCoinValue?.Invoke(m_CoinsCount);
    }

    public ERank GetLevelRank()
    {

        // ça il est juste appelé quand le niveau est fini,
        int rank = 0;

        // On calcule le RANK
        if (!m_IsPlayerDied) rank++;
        rank += Mathf.FloorToInt(3 * m_CoinsCount / m_CoinsMax);

        // Show le progress (Le rank)
        OnShowProgress?.Invoke((ERank)rank);

        //TODO Arrete la music atuel
        //TODO Fait jouer la music de vitoire

        // Arrete le moviment du player
        m_IsPlaying = false;

        return (ERank) rank;
    }
}
