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

    private CinemachineBrain m_CinemachineBrain;
    //private 



    // TODO : Ajouter la rotation de la camera par rapport le axis Y
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponentInChildren<Animator>();
        // m_CinemachineBrain = CinemachineCore.Instance.GetActiveBrain(0); // je sais qu'il y a juste un BRAIN
        SubscribeAll();
    }
    private void SubscribeAll()
    {
        PlayerGroundManager.Instance.OnGoundEnter += OnGroundEnter;
    }

    void Update()
    {
        MoveForce();
        Jump();

    }


    private bool IsCameraFreeLockPressed()
    {
        return Input.GetMouseButton((int) MouseButton.Right);
    }

    private void MoveForce()
    {
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
            //velocity = directionDep * m_Speed + velocity.y * Vector3.up;
            m_Rigidbody.AddForce(directionDep * 3, ForceMode.Force);
        }

       //m_Rigidbody.velocity = velocity;
        m_Animator.SetFloat(GameParameters.AnimationPlayer.FLOAT_VELOCITY, m_Rigidbody.velocity.magnitude);
    }


    private void Move()
    {
        // if (m_IsInterracting) return;

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
            velocity = directionDep * m_Speed + velocity.y * Vector3.up;
        }

        m_Rigidbody.velocity = velocity;
        m_Animator.SetFloat(GameParameters.AnimationPlayer.FLOAT_VELOCITY, velocity.magnitude);
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
            m_Rigidbody.AddForce(m_JumpForce * Vector3.up, ForceMode.Impulse);
           // m_IsGrounded = false;
        }
    }

    private void OnGroundEnter()
    {
        m_IsGrounded = true;
    }

}
