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
    private SpriteRenderer m_SpriteRenderer;
    private Rigidbody2D m_Rigidbody;
    private bool m_IsLookingRight;

    private event Action<int> m_OnChangeCurrentLevelId;

    private static PlayerMenuController m_Instance;

    public static PlayerMenuController GetInstance()
    {
        return m_Instance;
    }

    private void Awake()
    {
        if (PlayerMenuController.m_Instance != null)
        {
            Destroy(gameObject);
        }

        PlayerMenuController.m_Instance = this;
    }

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_PlayerAnimator = GetComponentInChildren<Animator>();
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_IsLookingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        NotifyAnimationSpeed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTER TRIGGER " + collision.tag);
        Spot spot = collision.GetComponent<Spot>();
        if (spot != null)
        {
            m_Rigidbody.velocity = Vector2.zero; // Stop

            //Update current LevelId
            int currentLevelId = spot.GetLevelId();
            m_OnChangeCurrentLevelId?.Invoke(currentLevelId);

            if (!m_IsLookingRight) DoFlip();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(GameParameters.TagName.SPOT)) 
        {
            // TODO Clean Menu and Level description
        }
    }

    private void Move()
    {
        if (MenuStateManager.GetInstance().IsMainMenu()) return;

        float axisHorizontal = Input.GetAxis(GameParameters.InputName.AXIS_HORIZONTAL);
        Move(axisHorizontal);
    }

    private void Move(float axisHorizontal)
    {
        Vector2 velocity = m_Rigidbody.velocity;
        if (velocity.magnitude > 0)
            return;

        if (!MenuStateManager.GetInstance().IsLastLevel() && axisHorizontal > 0) velocity = transform.right * m_Speed;
        else if (!MenuStateManager.GetInstance().IsMainMenu() && axisHorizontal < 0) velocity = transform.right * m_Speed * -1;

        m_Rigidbody.velocity = velocity;
        FlipPlayer(axisHorizontal);
    }

    private void NotifyAnimationSpeed()
    {
        m_PlayerAnimator.SetFloat("velocity", m_Rigidbody.velocity.magnitude);
    }

    private void FlipPlayer(float axisHorizontal)
    {
        if ((axisHorizontal < 0 && m_IsLookingRight) || (axisHorizontal > 0 && !m_IsLookingRight)) 
        {
            DoFlip();
        }
    }

    private void DoFlip()
    {
        m_IsLookingRight = !m_IsLookingRight;
        m_SpriteRenderer.flipX = !m_SpriteRenderer.flipX;
    }

    public void OnClickLevelSelect()
    {
        Move(0.5f);
    }

    public void SubscribeOnChangeCurrentLevelId(Action<int> action)
    {
        m_OnChangeCurrentLevelId += action;
    }
}
