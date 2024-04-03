using UnityEngine;



public class CoinController : MonoBehaviour
{
    [SerializeField] private Vector2 m_ValueRange = new Vector2(5, 10);

    private int m_Value;

    private void Start()
    {
        GenerateRandomValue();
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

    private void GenerateRandomValue()
    {
        m_Value = Mathf.FloorToInt(Random.Range(m_ValueRange.x, m_ValueRange.y));
    }

}
