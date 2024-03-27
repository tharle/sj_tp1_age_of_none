using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float m_PlayerSpeed = 2.0f;
    [SerializeField] private float m_JumpHeight = 1.0f;

    private CharacterController m_Controller;
    private Vector3 m_PlayerVelocity;
    private bool m_GroundedPlayer;
    private float m_GravityValue = -9.81f;

    private InputManager m_InputManager;


    private void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_InputManager = InputManager.Instance;
    }

    void Update()
    {
        m_GroundedPlayer = m_Controller.isGrounded;
        if (m_GroundedPlayer && m_PlayerVelocity.y < 0)
        {
            m_PlayerVelocity.y = 0f;
        }

        Vector2 movement = m_InputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);
        m_Controller.Move(move * Time.deltaTime * m_PlayerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (m_InputManager.IsPlayerJumpedThisFrame() && m_GroundedPlayer)
        {
            m_PlayerVelocity.y += Mathf.Sqrt(m_JumpHeight * -3.0f * m_GravityValue);
        }

        m_PlayerVelocity.y += m_GravityValue * Time.deltaTime;
        m_Controller.Move(m_PlayerVelocity * Time.deltaTime);
    }
}
