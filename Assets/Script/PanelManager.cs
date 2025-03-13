using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject farmPanel;
    public GameObject puzzlePanel;
    public GameObject shopPanel;
    public GameObject inventoryPanel;

    void Start()
    {
        ShowPanel(null); // ��͹�ء Panel �͹�������
    }

    public void ShowPanel(GameObject panel)
    {
        Debug.Log("Switching panel: " + (panel != null ? panel.name : "None"));

        // �Դ�ء Panel ��͹
        if (farmPanel != null) farmPanel.SetActive(false);
        if (puzzlePanel != null) puzzlePanel.SetActive(false);
        if (shopPanel != null) shopPanel.SetActive(false);
        if (inventoryPanel != null) inventoryPanel.SetActive(false);

        // �Դ Panel ������͡
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }
}
