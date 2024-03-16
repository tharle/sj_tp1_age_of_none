using System;
using UnityEngine;


public class PlayerMenuController : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    private Animator m_PlayerAnimator;
    private SpriteRenderer m_SpriteRenderer;
    private Rigidbody2D m_Rigidbody;
    private bool m_IsLookingRight;

    private event Action<int> m_OnChangeCurrentLevelId;
    private event Action m_OnMoveToNextSpot;

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
        Spot spot = collision.GetComponent<Spot>();
        if (spot != null)
        {
            m_OnMoveToNextSpot?.Invoke();
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
        m_PlayerAnimator.SetFloat(GameParameters.AnimationPlayer.FLOAT_VELOCITY, m_Rigidbody.velocity.magnitude);
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

    // ----------------------------------------
    // ACTIONS
    // ----------------------------------------

    public void SubscribeOnChangeCurrentLevelId(Action<int> action)
    {
        m_OnChangeCurrentLevelId += action;
    }

    public void SubscribeOnMoveToNextSpot(Action action)
    {
        m_OnMoveToNextSpot += action;
    }

    public void MoveLeft()
    {
        Move(-0.5f); // Hardcode Juste pour donner la direction
    }

    public void MoveRight()
    {
        Move(0.5f); // Hardcode Juste pour donner la direction
    }
}
