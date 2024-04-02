using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform m_ToFolow;

    private Vector3 m_Distance;

    private void Start()
    {
        m_Distance = transform.position - m_ToFolow.position;
    }

    private void Update()
    {
        transform.position = m_ToFolow.position + m_Distance;
    }
}
