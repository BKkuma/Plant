using System.Collections.Generic;
using UnityEngine;
using TMPro; // ใช้ TextMeshPro สำหรับแสดงผล

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public TextMeshProUGUI inventoryText; // ตัว Text ที่ใช้แสดงข้อมูล

    private void Start()
    {
        inventoryPanel.SetActive(false); // เริ่มต้นให้ Inventory ปิด
    }

    public void ToggleInventory()
    {
        bool isActive = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isActive);

        if (isActive)
        {
            UpdateInventoryDisplay();
        }
    }

    private void UpdateInventoryDisplay()
    {
        Dictionary<string, int> items = InventoryManager.Instance.GetInventory();
        inventoryText.text = ""; // เคลียร์ข้อความก่อน

        foreach (var item in items)
        {
            inventoryText.text += $"{item.Key}: {item.Value} pt\n";
        }

        if (items.Count == 0)
        {
            inventoryText.text = "not item in storage";
        }
    }
}
