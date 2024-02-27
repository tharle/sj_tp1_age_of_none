using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{

    private int m_LevelId = -1;

    public void SetLevelId(int indexLevel) {  m_LevelId = indexLevel; }
    public int GetLevelId() { return m_LevelId; }
}
