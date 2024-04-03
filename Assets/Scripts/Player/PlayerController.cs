using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private int m_CountCoins;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (FindAnyObjectByType<PlayerController>() != this)
            Destroy(gameObject);
    }

    public void CollectCoin(int value)
    {
        Debug.Log($"We collect {value} gold. ");
        m_CountCoins += value;
    }
}
