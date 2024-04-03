using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HUDCoinManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI m_CoinValue;

    private void Start()
    {
        SubscribleAllActions();
    }

    private void SubscribleAllActions()
    {
        PlayerController.Instance.OnChangeCoinValue += OnChangeCoinValue;
    }

    private void OnChangeCoinValue(int value)
    {
        m_CoinValue.text = $"x {value.ToString("00")}";
    }
}
