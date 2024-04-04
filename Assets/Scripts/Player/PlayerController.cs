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
        int rank = 0;

        if (!m_IsPlayerDied) rank++;

        rank += Mathf.FloorToInt(3 * m_CoinsCount / m_CoinsMax);

        OnShowProgress?.Invoke((ERank)rank);

        return (ERank) rank;
    }
}
