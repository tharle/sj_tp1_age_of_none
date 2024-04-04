using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static Cinemachine.DocumentationSortingAttribute;

[Serializable]
public class SaveData
{
    public long DateTimeInBinary;
    public Player PlayerData;

    public DateTime Date { get { return DateTime.FromBinary(DateTimeInBinary);} }

    public SaveData()
    {
        DateTimeInBinary = DateTime.Now.ToBinary();
        PlayerData = new Player();
        PlayerData.Achievements = new AchievementData[0];
        PlayerData.Levels = new List<LevelHistoric>(); 
    }
}
