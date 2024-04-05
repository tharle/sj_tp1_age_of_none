using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Level;

public class HUDManager : MonoBehaviour
{

    [SerializeField] GameObject m_MainMenuGameObject;
    [SerializeField] GameObject m_LevelMenuGameObject;
    [SerializeField] LevelMenuController m_LevelController;

    // Element level
    [SerializeField] Image m_RankImage;
    [SerializeField] TextMeshProUGUI m_TitleText;
    [SerializeField] TextMeshProUGUI m_DescriptionText;

    [SerializeField] GameObject m_BtStart;
    [SerializeField] GameObject m_BtNextText;

    [SerializeField] Button m_BtNextLevel;

    [SerializeField] Animator m_MainMenuAnimator;
    [SerializeField] Animator m_LevelMenuAnimator;

    private BundleLoader m_Loader;

    private string m_Description;
    private int m_LastPos;
    private Coroutine m_TypeWriteRoutine;
    private bool m_IsTyping;
    private bool m_IsEndText;
    private Level.EType m_EType;

    Dictionary<ERank, Sprite> m_RankStamps = new Dictionary<ERank, Sprite>();

    private void Start()
    {
        m_Loader = BundleLoader.Instance;
        SubscribleAllActions();
        LoadAllRankStamps();
    }

    private void Update()
    {
        if (Input.GetKeyDown(GameParameters.InputName.NEXT_TEXT)) OnNextText();
    }

    private void LoadAllRankStamps()
    {
        m_RankStamps = m_Loader.LoadAllRankStamps();
    }
    public void OnNextText()
    {
        if (!m_IsTyping && !m_IsEndText && !MenuStateManager.GetInstance().IsMainMenu()) 
        { 
            m_TypeWriteRoutine = StartCoroutine(TypeWriteRoutine());
            AudioManager.GetInstance().Play(EAudio.SFXText, Input.mousePosition);
        }
    }

    public void OnClickStartLevel()
    {
        switch (m_EType)
        {
            case EType.Level1:
                SceneController.OnClickLevel1();
                break;
            case EType.Level2:
                SceneController.OnClickLevel2();
                break;
            case EType.Level3:
                SceneController.OnClickLevel3();
                break;
        }
    }

    // ----------------------------------------
    // ACTIONS
    // ----------------------------------------

    private void SubscribleAllActions()
    {
        m_LevelController.SubscribeOnToggleMainMenu(OnToggleMainMenu);
        m_LevelController.SubscribeOnToggleLevelMenu(OnToggleLevelMenu);
    }

    private void OnToggleMainMenu(bool show)
    {
        if(m_MainMenuGameObject != null && !m_MainMenuGameObject.IsDestroyed()) m_MainMenuGameObject.SetActive(show);

        if (show) m_MainMenuAnimator.SetTrigger(GameParameters.AnimationMenu.TRIGGER_OPEN);
        else m_MainMenuAnimator.SetTrigger(GameParameters.AnimationMenu.TRIGGER_CLOSE);
    }

    private void OnToggleLevelMenu(bool show, Level level)
    {
        //Fix pour quand je ferme le unity avant
        if (m_LevelMenuGameObject.IsDestroyed()) return;

        m_LevelMenuGameObject.SetActive(show);

        if (!show) 
        {
            StopCoroutine(m_TypeWriteRoutine);
            m_LevelMenuAnimator.SetTrigger(GameParameters.AnimationMenu.TRIGGER_OPEN);
            return;
        }

        FillLevelInfos(level);
    }

    private void FillLevelInfos(Level level) 
    {
        m_TitleText.text = level.Title;
        m_RankImage.sprite = m_RankStamps[level.RankId];

        m_Description = level.Description;
        m_LastPos = 0;
        m_EType = level.TypeId;

        m_TypeWriteRoutine =  StartCoroutine(TypeWriteRoutine());

    }

    private void ChangeButtons()
    {
        m_BtNextText.SetActive(m_LastPos > 0 && !m_IsTyping && !m_IsEndText);
        m_BtStart.SetActive(m_IsEndText);
        m_BtNextLevel.enabled = !MenuStateManager.GetInstance().IsLastLevel();
    }

    private IEnumerator TypeWriteRoutine()
    {
        m_IsTyping = true;
        m_IsEndText = false;
        m_DescriptionText.text = "";
        ChangeButtons();
        for (int i= m_LastPos; i < m_Description.Length; i++)
        {
            char c = m_Description[i];
            if (c == GameParameters.TypeWriteConfiguration.BREAK_LINE)
            {
                m_LastPos = i + 1;
                break;
            }

            if(i == (m_Description.Length - 1))
            {
                m_IsEndText = true;
            }

            m_DescriptionText.text += c;
            yield return new WaitForSeconds(0.05f); // TODO ajouter dans configuration
        }
        m_IsTyping = false;
        ChangeButtons();
    }
}
