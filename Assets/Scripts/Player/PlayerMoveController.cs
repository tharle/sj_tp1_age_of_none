using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] private float m_Speed = 5.0f;
    [SerializeField] private float m_JumpForce = 1.0f;
    [SerializeField] private Transform m_CameraTransform;
    [SerializeField] private bool m_IsGrounded = true;

    private Animator m_Animator;
    private Rigidbody m_Rigidbody;

    private PlayerController m_PlayerController;
    private CinemachineBrain m_CinemachineBrain;
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponentInChildren<Animator>();
        m_PlayerController = PlayerController.Instance;
        SubscribeAll();
    }
    private void SubscribeAll()
    {
        PlayerGroundManager.Instance.OnGoundEnter += OnGroundEnter;
    }

    void Update()
    {
        if (!m_PlayerController.IsPlaying)
        {
            // Fait le Rigibody arreter
            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.useGravity = false;
            m_Animator.SetFloat(GameParameters.AnimationPlayer.FLOAT_VELOCITY, 0);
            return;
        }

        Move();
        Jump();
    }

    private void Move()
    {

        // Petit truc pour ne pas bouger quand il fait l'animation de "Stop Running"
        AnimatorClipInfo[] animatorinfo = m_Animator.GetCurrentAnimatorClipInfo(0);
        if (animatorinfo[0].clip.name == GameParameters.AnimationPlayer.NAME_RUN_TO_STOP)  
        {
            Vector3 velocityTemp = m_Rigidbody.velocity;
            velocityTemp.y = 0;
            m_Animator.SetFloat(GameParameters.AnimationPlayer.FLOAT_VELOCITY, velocityTemp.magnitude);
            return;
        }


        // obtient les valeurs des touches horizontales et verticales
        float hDeplacement = Input.GetAxis(GameParameters.InputName.AXIS_HORIZONTAL);
        float vDeplacement = Input.GetAxis(GameParameters.InputName.AXIS_VERTICAL);

        //obtient la nouvelle direction ( (avant/arrièrre) + (gauche/droite) )
        Vector3 directionDep = GetCameraTransform().forward * vDeplacement + GetCameraTransform().right * hDeplacement;
        directionDep.y = 0; //pas de valeur en y , le cas où la caméra regarde vers le bas ou vers le haut
        Vector3 velocity = Vector3.zero;
        if (directionDep != Vector3.zero) //change de direction s’il y a un changement
        {
            //Oriente le personnage vers la direction de déplacement et applique la vélocité dans la même direction
            transform.forward = directionDep;
            
            velocity = directionDep * m_Speed;
        }
        m_Animator.SetFloat(GameParameters.AnimationPlayer.FLOAT_VELOCITY, velocity.magnitude);
        velocity.y = m_Rigidbody.velocity.y;
        m_Rigidbody.velocity = velocity;
    }

    private Transform GetCameraTransform()
    {
        // return m_CinemachineBrain.transform;
        return m_CameraTransform;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(GameParameters.InputName.PLAYER_JUMP) && m_IsGrounded)
        {
            AudioManager.GetInstance().Play(EAudio.SFXJump, transform.position);
            m_Rigidbody.AddForce(m_JumpForce * Vector3.up, ForceMode.Impulse);
            m_IsGrounded = false;
            m_Animator.SetBool(GameParameters.AnimationPlayer.BOOL_IS_GROUNDED, m_IsGrounded);
        }
    }

    private void OnGroundEnter()
    {
        Debug.Log("ON GROUN ENTER!!!");
        m_IsGrounded = true;
        m_Animator.SetBool(GameParameters.AnimationPlayer.BOOL_IS_GROUNDED, m_IsGrounded);
    }

}
