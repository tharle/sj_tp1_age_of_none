using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerController : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    public float Speed { set { m_Speed = value; } }

    [SerializeField] private float m_LifeTime = 4;
    public float LifeTime { set { m_LifeTime = value; } }
    
    [SerializeField] private EAudio m_AudioId = EAudio.SFXFireBall;
    public EAudio AudioId { set { m_AudioId = value; } }

    float m_Time;

    private void Start()
    {
        m_Time = Time.time;
    }


    private void OnEnable()
    {
        m_Time = Time.time;
    }

    private void Update()
    {
        if(Time.time - m_Time > m_LifeTime) gameObject.SetActive(false);

        transform.Translate(transform.forward * m_Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        AudioManager.GetInstance().Play(m_AudioId, transform.position);
        if (player)
        {
            player.DoDie();
        }
    }
}
