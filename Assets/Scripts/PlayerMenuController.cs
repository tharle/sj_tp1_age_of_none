using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMenuController : MonoBehaviour
{
    [SerializeField] public List<GameObject> m_LevelsPrefabs;
    [SerializeField] private float m_Speed;
    private Animator m_PlayerAnimator;
    private SpriteRenderer m_SpriteRenderer;
    private Rigidbody2D m_Rigidbody;
    private bool m_IsLookingRight;
    private int m_IndexCurrentLevel; // 0 is menu, 1...N is Levels

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
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_IsLookingRight = true;
        m_IndexCurrentLevel = 0;
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
            m_IndexCurrentLevel = spot.GetIndexLevel();
            if (m_IndexCurrentLevel == 0) OnStopInMainMenu();
            else OnStopInLevel();

            if (!m_IsLookingRight) DoFlip();
        }
    }

    private void OnStopInLevel()
    {
        m_Rigidbody.velocity = Vector2.zero;
        // TODO change to Main menu
    }

    private void OnStopInMainMenu()
    {
        m_Rigidbody.velocity = Vector2.zero;
        // TODO change level description
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
        Vector2 velocity = m_Rigidbody.velocity;
        if (velocity.magnitude > 0)
            return;

        float axisHorizontal = Input.GetAxis(GameParameters.InputName.AXIS_HORIZONTAL);

        if (!IsLastLevel() && axisHorizontal > 0) velocity = transform.right * m_Speed;
        else if (!IsMainMenu() && axisHorizontal < 0) velocity = transform.right * m_Speed * -1;

        m_Rigidbody.velocity = velocity;
        FlipPlayer(axisHorizontal);
    }

    public bool IsMainMenu()
    {
        return m_IndexCurrentLevel <= 0;
    }

    public bool IsLastLevel()
    {
        return m_IndexCurrentLevel == (CountLevels() - 1);
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


    public int CountLevels()
    {
        return m_LevelsPrefabs.Count;
    }

    public GameObject GetLevelPrefabAt(int indexLevel)
    {
        return m_LevelsPrefabs[indexLevel];
    }
}
