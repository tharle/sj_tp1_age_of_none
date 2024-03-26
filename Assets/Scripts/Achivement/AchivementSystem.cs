using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum EAchievementFlag
{
    GoblinsKilledCount,
    PlayerDeathCount
}
public class AchivementSystem : MonoBehaviour
{
    // Variables membres
    private Achivement[] m_Achivements = new Achivement[0];
    private Dictionary<EAchievementFlag, int> m_AchivementFlagTracker = new Dictionary<EAchievementFlag, int>();

    //Actions
    public event Action<Achivement[], Dictionary<EAchievementFlag, int>> OnAchivementChange;

    // Prefabs
    private AchivementEntryVisual m_AchEntryPrefab;


    //Instance
    private static AchivementSystem m_Instance;
    public static AchivementSystem Instance
    {
        get
        {
            if(m_Instance == null)
            {
                Canvas canvas = FindFirstObjectByType<Canvas>();
                m_Instance = canvas.AddComponent<AchivementSystem>();
            }

            return m_Instance;
        }
    }

    private void Start()
    {
        m_AchEntryPrefab = Resources.Load<AchivementEntryVisual>("Prefabs/Achivements/AchivementEntryVisual");
        m_AchEntryPrefab.SetCanvas(GetComponent<Canvas>());
    }

    private void ScanAchivementUnlocked(EAchievementFlag achievementFlagId)
    {
        for (int i = 0; i < m_Achivements.Length; i++) 
        {
            Achivement archivement = m_Achivements[i];
            if (!archivement.Unlocked && archivement.VerifyAndUnlock(this))
            {
                m_Achivements[i] = archivement;
                UnlockAchievement(archivement);
            }
        }
    }
    private void UnlockAchievement(Achivement data)
    {
        AchivementEntryVisual newEntry = Instantiate(m_AchEntryPrefab, transform);
        newEntry.Data = data;

        // Save
        OnAchivementChange?.Invoke(m_Achivements, m_AchivementFlagTracker);
    }


    public void Load(Achivement[] achivements)
    {
        Load(achivements, new Dictionary<EAchievementFlag, int>());
    }

    public void Load(Achivement[] achivements, Dictionary<EAchievementFlag, int> achivementFlagTracker)
    {
        m_Achivements = achivements;
        m_AchivementFlagTracker = achivementFlagTracker;
    }

    public void AddProgress(EAchievementFlag achievementId)
    {
        if (!m_AchivementFlagTracker.ContainsKey(achievementId)) m_AchivementFlagTracker.Add(achievementId, 0);

        m_AchivementFlagTracker[achievementId] += 1;

        ScanAchivementUnlocked(achievementId);
    }

    public int GetArchivementFlagValue(EAchievementFlag achievementFlagId)
    {
        return m_AchivementFlagTracker.ContainsKey(achievementFlagId)? m_AchivementFlagTracker[achievementFlagId] : 0;
    }
}
