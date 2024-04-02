using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundManager : MonoBehaviour
{

    public Action OnGoundEnter;

    private static PlayerGroundManager m_Instance;
    
    public static PlayerGroundManager Instance
    {
        get { return m_Instance; }
    }

    private void Awake()
    {
        if(m_Instance != null && m_Instance != this) Destroy(gameObject);

        m_Instance = this;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collition : "+ collision.collider.tag);
        if (collision.collider.CompareTag(GameParameters.TagName.GROUND))
        {
            Debug.Log("IN COLLISION WITH GROUND!!!");
            OnGoundEnter?.Invoke();
        }
        


    }
}
