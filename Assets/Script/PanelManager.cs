using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject farmPanel;
    public GameObject puzzlePanel;
    public GameObject shopPanel;
    public GameObject inventoryPanel;
    public FarmManager farmManager; // ✅ อ้างอิง FarmManager
    public GameObject seedUI;

    void Start()
    {
        ShowPanel(null);
    }

    public void ShowPanel(GameObject panel)
    {
        farmPanel?.SetActive(false);
        puzzlePanel?.SetActive(false);
        shopPanel?.SetActive(false);
        inventoryPanel?.SetActive(false);

        if (panel != null)
        {
            panel.SetActive(true);

            // ✅ ถ้าเป็น FarmPanel ให้เปิดฟาร์ม & แสดงพืช
            if (farmManager != null)
            {
                farmManager.SetFarmGridActive(panel == farmPanel);
            }

            if (seedUI != null)
            {
                seedUI.SetActive(panel == farmPanel); // แสดง Seed UI เฉพาะตอนเปิด Farm Panel
            }

        }
        else
        {
            // ✅ ถ้าไม่มี Panel ให้ซ่อนพืช
            if (farmManager != null)
            {
                farmManager.SetFarmGridActive(false);

            }
            if (seedUI != null) seedUI.SetActive(false);
        }
    }
}
