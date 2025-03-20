using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // เพิ่มไอเทมเข้าคลัง
    public void AddItem(string itemName, int amount = 1)
    {
        if (inventory.ContainsKey(itemName))
        {
            inventory[itemName] += amount;
        }
        else
        {
            inventory[itemName] = amount;
        }

        Debug.Log($"📦 เพิ่ม {itemName} จำนวน {amount} ชิ้น (รวม: {inventory[itemName]})");
    }

    // ดึงข้อมูลไอเทมทั้งหมดใน inventory
    public Dictionary<string, int> GetInventory()
    {
        return inventory;
    }
}
