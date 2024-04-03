using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player m_Player;

    private int m_CountCoins;

    public event Action<int> OnChangeCoinValue;

    private static PlayerController m_Instance;
    public static PlayerController Instance { get { return m_Instance; } }

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
            Destroy(gameObject);

        m_Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        m_CountCoins = 0;
    }

    public void CollectCoin(int value)
    {
        Debug.Log($"We collect {value} gold. ");
        m_CountCoins += value;
        OnChangeCoinValue?.Invoke(m_CountCoins);
    }
}
