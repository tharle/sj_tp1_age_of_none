using Unity.VisualScripting;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform m_ToFolow;
    [SerializeField] private float m_Angle = 15f;

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
        RotateCamera();
    }

    private void RotateCamera()
    {
        if (Input.GetMouseButton((int)MouseButton.Right))
        {
            Vector3 eulerAngle = transform.rotation.eulerAngles;
            eulerAngle.y += m_Angle * Input.GetAxis("Mouse X"); // Horizontal camera
            eulerAngle.x += m_Angle * Input.GetAxis("Mouse Y"); // Vertical camera
            
            eulerAngle.y = eulerAngle.y > 360 ? eulerAngle.y - 360 : eulerAngle.y;
            eulerAngle.y = eulerAngle.y < 0 ? eulerAngle.y + 360 : eulerAngle.y;
            
            //eulerAngle.x = eulerAngle.x > 40 ? 40 : eulerAngle.x;
            //eulerAngle.x = eulerAngle.x < -45 ? -45 : eulerAngle.x;
            transform.rotation = Quaternion.Euler(eulerAngle.x, eulerAngle.y, eulerAngle.z);
        }
    }
}
