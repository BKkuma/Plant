using System.Collections.Generic;
using UnityEngine;
using TMPro; // �� TextMeshPro ����Ѻ�ʴ���

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public TextMeshProUGUI inventoryText; // ��� Text ������ʴ�������

    private void Start()
    {
        inventoryPanel.SetActive(false); // ���������� Inventory �Դ
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
        inventoryText.text = ""; // �������ͤ�����͹

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
