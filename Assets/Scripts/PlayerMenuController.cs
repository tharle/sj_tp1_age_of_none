using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMenuController : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    private Animator m_PlayerAnimator;
    private Rigidbody2D m_Rigidbody;
    private bool m_LookingRight;

    private static PlayerMenuController m_Instance;

    public static PlayerMenuController GetInstance() 
    { 
        return m_Instance; 
    }

    private void Awake()
    {
        if(m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_Instance = this;
    }

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_PlayerAnimator = GetComponentInChildren<Animator>();
        m_LookingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        float axisHorizontal = Input.GetAxis("Horizontal");

        FlipPlayer(axisHorizontal);

        Vector3 velocity = transform.right * axisHorizontal * m_Speed;
        m_Rigidbody.velocity = velocity;
        m_PlayerAnimator.SetFloat("velocity", velocity.magnitude);
    }

    private void FlipPlayer(float axisHorizontal)
    {
        Vector3 scale = transform.localScale;
        if (axisHorizontal < 0 && m_LookingRight) 
        { 
            m_LookingRight = false;
            scale.x *= -1; // flip
        }

        if (axisHorizontal > 0 && !m_LookingRight)
        {
            m_LookingRight = true;
            scale.x *= -1; // flip
        }
        transform.localScale = scale;
    }
}
