using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    [SerializeField] GameObject m_MainMenuGameObject;
    [SerializeField] GameObject m_LevelMenuGameObject;
    [SerializeField] LevelMenuController m_LevelController;

    // Element level
    [SerializeField] Image m_RankImage;
    [SerializeField] TextMeshProUGUI m_TitleText;
    [SerializeField] TextMeshProUGUI m_DescriptionText;

    private string m_Description;
    private int m_LastPos;
    private Coroutine m_TypeWriteRoutine;
    private bool m_IsTyping;
    private bool m_IsEndText;

    Dictionary<ERank, Sprite> m_RankStamps = new Dictionary<ERank, Sprite>();

    private void Start()
    {
        SubscribleAllActions();
        LoadAllRankStamps();
    }

    private void Update()
    {
        if (Input.GetKeyDown(GameParameters.InputName.NEXT_TEXT)) NextText();
    }

    private void NextText()
    {
        if(!m_IsTyping && !m_IsEndText) m_TypeWriteRoutine = StartCoroutine(TypeWriteRoutine());
    }

    private void LoadAllRankStamps()
    {
        m_RankStamps = new Dictionary<ERank, Sprite>();

        m_RankStamps[ERank.S] = Resources.Load<Sprite>(GameParameters.Directory.RESOURCES_RANK+ "S");
        m_RankStamps[ERank.A] = Resources.Load<Sprite>(GameParameters.Directory.RESOURCES_RANK+ "A");
        m_RankStamps[ERank.B] = Resources.Load<Sprite>(GameParameters.Directory.RESOURCES_RANK+ "B");
        m_RankStamps[ERank.C] = Resources.Load<Sprite>(GameParameters.Directory.RESOURCES_RANK+ "C");
        m_RankStamps[ERank.NONE] = Resources.Load<Sprite>(GameParameters.Directory.RESOURCES_RANK + "NONE");

    }

    // ----------------------------------------
    // ACTIONS
    // ----------------------------------------

    private void SubscribleAllActions()
    {
        m_LevelController.SubscribeOnToggleMainMenu(OnToggleMainMenu);
        m_LevelController.SubscribeOnToggleLevelMenu(OnToggleLevelMenu);
    }

    public void OnToggleMainMenu(bool show)
    {
        m_MainMenuGameObject.SetActive(show);
    }

    public void OnToggleLevelMenu(bool show, LevelData.Level level)
    {
        m_LevelMenuGameObject.SetActive(show);

        if (!show) 
        {
            StopCoroutine(m_TypeWriteRoutine);
            return;
        }

        FillLevelInfos(level);
    }

    private void FillLevelInfos(LevelData.Level level) 
    {
        m_TitleText.text = level.Title;
        m_RankImage.sprite = m_RankStamps[level.RankId];

        m_Description = level.Description;
        m_LastPos = 0;
        UpdateNextText(false);
        
        m_TypeWriteRoutine =  StartCoroutine(TypeWriteRoutine());

    }

    private void UpdateNextText(bool isNextText)
    {

    }

    private IEnumerator TypeWriteRoutine()
    {
        m_IsTyping = true;
        m_IsEndText = false;
        m_DescriptionText.text = "";
        for (int i= m_LastPos; i < m_Description.Length; i++)
        {
            char c = m_Description[i];
            if (c == GameParameters.TypeWriteConfiguration.BREAK_LINE)
            {
                m_LastPos = i + 1;
                UpdateNextText(true);
                break;
            }

            if(i == (m_Description.Length - 1))
            {
                m_IsEndText = true;
                UpdateNextText(false);
            }

            m_DescriptionText.text += c;
            yield return new WaitForSeconds(0.05f); // TODO ajouter dans configuration
        }
        m_IsTyping = false;
    }
}
