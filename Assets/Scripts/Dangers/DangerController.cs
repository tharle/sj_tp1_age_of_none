using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerController : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    public float Speed { set { m_Speed = value; } }

    [SerializeField] private float m_LifeTime = 4;
    public float LifeTime { set { m_LifeTime = value; } }
    
    [SerializeField] private EAudio m_AudioPlayer = EAudio.SFXDamaged;
    public EAudio AudioPlayer { set { m_AudioPlayer = value; } }

    [SerializeField] private EAudio m_AudioCast = EAudio.SFXFireBall;
    public EAudio AudioCast { set { m_AudioCast = value; } }

    float m_Time;

    private void Start()
    {
        CastDanger();
    }
    private void OnEnable()
    {
        CastDanger();
    }

    private void CastDanger()
    {
        AudioManager.GetInstance().Play(m_AudioCast, transform.position, false, 0.1f);
        m_Time = Time.time;
    }



    private void Update()
    {
        if(m_LifeTime >= 0 && Time.time - m_Time > m_LifeTime) gameObject.SetActive(false);

        transform.Translate(transform.forward * m_Speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        AudioManager.GetInstance().Play(m_AudioPlayer, transform.position);
        if (player)
        {
            player.DoDie();
        }
    }
}
