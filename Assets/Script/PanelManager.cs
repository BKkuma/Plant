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
        // ไม่ต้องปิดทุก Panel ตั้งแต่เริ่ม
        CloseAllPanels();
    }

    public void ShowPanel(GameObject panel)
    {
        // กรณีปิด FarmPanel
        if (panel == farmPanel)
        {
            bool isActive = farmPanel.activeSelf;
            farmPanel.SetActive(!isActive);
            farmManager.SetFarmGridActive(!isActive);
            if (seedUI != null) seedUI.SetActive(!isActive);
            return;
        }

        // กรณีปิด PuzzlePanel
        if (panel == puzzlePanel)
        {
            PuzzleGridManager puzzleManager = FindObjectOfType<PuzzleGridManager>();
            if (puzzleManager != null)
            {
                puzzleManager.TogglePuzzleGrid();
            }
            return;
        }

        // ปิดทุก Panel แล้วเปิด Panel ที่ต้องการ
        CloseAllPanels();
        if (panel != null) panel.SetActive(true);
    }

    void CloseAllPanels()
    {
        farmPanel?.SetActive(false);
        puzzlePanel?.SetActive(false);
        shopPanel?.SetActive(false);
        inventoryPanel?.SetActive(false);
    }
}
