using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ERank
{
    NONE,
    C,
    B,
    A,
    S
}

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public List<Level> levels = new List<Level>();
    
    [Serializable]    
    public struct Level
    {
        public string Title;
        public string Description;
        public ERank RankId;
        public string NamePrefab;
    }
}
