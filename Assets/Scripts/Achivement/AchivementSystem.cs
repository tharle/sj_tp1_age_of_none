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
    PlayerDeathCount
}
public class AchivementSystem : MonoBehaviour
{
    BundleLoader m_Loader;

    // Variables membres
    private Achivement[] m_Achivements = new Achivement[0];
    private Dictionary<EAchievementFlag, int> m_AchivementFlagTracker = new Dictionary<EAchievementFlag, int>();

    //Actions
    public event Action<Achivement[]> OnAchivementChange;


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
        AchivementEntryVisual newEntry = LoadEntry();
        newEntry = Instantiate(newEntry, transform);
        newEntry.Data = data;

        // Save
        OnAchivementChange?.Invoke(m_Achivements);
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
