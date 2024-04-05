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
        if (!PlayerController.Instance.IsPlaying) return;

        ToFollow();
        RotateCamera();
    }

    private void ToFollow()
    {
        float oldY = transform.position.y;
        Vector3 position = m_ToFolow.position + m_Distance;
        position.y = oldY;
        transform.position = position;
    }

    private void RotateCamera()
    {
        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            Vector3 eulerAngle = transform.rotation.eulerAngles;
            // Horizontal camera
            eulerAngle.y += m_Angle * Input.GetAxis(GameParameters.InputName.AXIS_MOUSE_HORIZONTAL); 
            eulerAngle.y = eulerAngle.y > 360 ? eulerAngle.y - 360 : eulerAngle.y;
            eulerAngle.y = eulerAngle.y < 0 ? eulerAngle.y + 360 : eulerAngle.y;            
            
            // J'ai eu des problèmes pour mettre de limite dans la camera Vertical
            // Donc, j'ai abandoné ça
            //eulerAngle.x += m_Angle * Input.GetAxis("Mouse Y"); // Vertical camera
            //eulerAngle.x = eulerAngle.x > 40 ? 40 : eulerAngle.x;
            //eulerAngle.x = eulerAngle.x < -45 ? -45 : eulerAngle.x;

            transform.rotation = Quaternion.Euler(eulerAngle.x, eulerAngle.y, eulerAngle.z);
        }
    }
}
