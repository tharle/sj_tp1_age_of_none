using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{

    private int m_IndexLevel = 0;

    public void SetIndexLevel(int indexLevel) {  m_IndexLevel = indexLevel; }
    public int GetIndexLevel() { return m_IndexLevel; }
}
