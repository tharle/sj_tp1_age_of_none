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
        float oldY = transform.position.y;
        Vector3 position = m_ToFolow.position + m_Distance;
        position.y = oldY;
        transform.position = position;
    }
}
