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


[Serializable]
public struct Level
{
    public enum EType
    {
        Level1,
        Level2,
        Level3
    }

    public EType TypeId;
    public string Title;
    public string Description;
    public ERank RankId;
    public string NamePrefab;
}

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    public List<Level> levels = new List<Level>();
}
