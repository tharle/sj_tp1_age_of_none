using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputs m_PlayerInputs;

    private static InputManager m_Instance;
    public static InputManager Instance { 
        get { 
            return m_Instance; 
        } 
    }

    private void Awake()
    {
        if(m_Instance != null && m_Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        m_Instance = this;
        m_PlayerInputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        m_PlayerInputs.Enable();
    }

    private void OnDisable()
    {
        m_PlayerInputs.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return m_PlayerInputs.Player.Movement.ReadValue<Vector2>();
    }

    public bool IsPlayerJumpedThisFrame()
    {
        return m_PlayerInputs.Player.Jump.triggered;
    }
}
