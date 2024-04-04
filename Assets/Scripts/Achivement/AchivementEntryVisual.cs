using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchivementEntryVisual : MonoBehaviour
{
    private Canvas m_Canvas;
    [SerializeField] private TextMeshProUGUI m_Title;
    [SerializeField] private TextMeshProUGUI m_Description;
    [SerializeField] private Image m_Icon;

    public AchievementData Data
    {
        set
        {
            m_Title.text = value.Title;
            m_Description.text = value.Description;
            m_Icon.sprite = value.Icon;
        }
    }

    private void Start()
    {
        Destroy(gameObject, 3);
    }

    public void SetCanvas(Canvas canvas)
    {
        m_Canvas = canvas;
    }

}
