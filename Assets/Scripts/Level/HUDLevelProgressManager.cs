using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDLevelProgressManager : MonoBehaviour
{
    [SerializeField] private GameObject m_ProgressPanel;
    [SerializeField] private Image m_RankStamp;

    private Dictionary<ERank, Sprite> m_RankStamps = new Dictionary<ERank, Sprite>();

    private void Start()
    {
        SubscribleAllActions();
        LoadAllRankStamps();
    }
    private void LoadAllRankStamps()
    {
        m_RankStamps = BundleLoader.Instance.LoadAllRankStamps();
    }

    private void SubscribleAllActions()
    {
        PlayerController.Instance.OnShowProgress += OnShowProgress;
    }

    private void OnShowProgress(ERank rankId)
    {
        m_ProgressPanel.SetActive(true);
        m_RankStamp.sprite = m_RankStamps[rankId];
    }

}
