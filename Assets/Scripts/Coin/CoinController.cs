using UnityEngine;



public class CoinController : MonoBehaviour
{

    private int m_Value;

    private void Start()
    {
        m_Value = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.CollectCoin(m_Value);
            Destroy(gameObject);
        }
    }
}
