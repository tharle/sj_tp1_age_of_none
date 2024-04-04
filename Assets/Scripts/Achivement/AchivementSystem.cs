using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static LevelData;

public enum EAchievementFlag
{
    GoblinsKilledCount,
    PlayerDeathCount,
    FinishedLevel1,
    FinishedLevel2,
    FinishedLevel3
}
public class AchivementSystem : MonoBehaviour
{
    BundleLoader m_Loader;

    // Variables membres
    private AchievementData[] m_Achivements = new AchievementData[0];
    private Dictionary<EAchievementFlag, int> m_AchivementFlagTracker = new Dictionary<EAchievementFlag, int>();

    //Actions
    public event Action<AchievementData[]> OnAchievementChange;


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
        m_Loader = BundleLoader.Instance;
    }

    private AchivementEntryVisual LoadEntry()
    {
        GameObject go = m_Loader.Load<GameObject>(GameParameters.BundleNames.PREFAB_ACHIVEMENTS, nameof(AchivementEntryVisual));
        AchivementEntryVisual newEntry = go.GetComponent<AchivementEntryVisual>();
        newEntry.SetCanvas(GetComponent<Canvas>());
        return newEntry;
    }

    private void ScanAchivementUnlocked(EAchievementFlag achievementFlagId)
    {
        for (int i = 0; i < m_Achivements.Length; i++) 
        {
            AchievementData archivement = m_Achivements[i];
            if (!archivement.Unlocked && archivement.VerifyAndUnlock(this))
            {
                m_Achivements[i] = archivement;
                UnlockAchievement(archivement);
            }
        }
    }
    private void UnlockAchievement(AchievementData data)
    {
        AchivementEntryVisual newEntry = LoadEntry();
        newEntry = Instantiate(newEntry, transform);
        newEntry.Data = data;

        // Save
        OnAchievementChange?.Invoke(m_Achivements);
    }


    public void Load(AchievementData[] achivements)
    {
        Dictionary<EAchievementFlag, int> achievementFlagTracker = new Dictionary<EAchievementFlag, int>();


        // LOAD conditions
        foreach(AchievementData achData in achivements)
        {
            foreach (AchivementCondition condition in achData.Conditions)
            {
                if (!condition.Unlocked) continue; // Si la condtion est pas débloque, on l'ignore
                 
                // Si la condtion est débloqué, on prends la valeur par défault de déblocage
                if (!achievementFlagTracker.ContainsKey(condition.AchievementFlagId))
                {
                    achievementFlagTracker.Add(condition.AchievementFlagId, condition.Value);
                }
            }
        }

        Load(achivements, achievementFlagTracker);
    }

    public void Load(AchievementData[] achivements, Dictionary<EAchievementFlag, int> achievementFlagTracker)
    {
        m_Achivements = achivements;
        m_AchivementFlagTracker = achievementFlagTracker;
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
