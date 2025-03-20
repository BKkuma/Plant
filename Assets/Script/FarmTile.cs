using UnityEngine;

public class FarmTile : MonoBehaviour
{
    public enum TileState { Empty, Planted, Watered, ReadyToHarvest }
    public TileState state = TileState.Empty;

    public GameObject currentPlant; // เก็บพืชที่ปลูกในช่องนี้

    private void OnMouseDown()
    {
        if (state == TileState.ReadyToHarvest)
        {
            Harvest(); // ถ้าพืชโตเต็มที่แล้ว ให้เก็บเกี่ยว
        }
        else if (state == TileState.Empty && SeedSelector.selectedSeed != null)
        {
            PlantSeed(SeedSelector.selectedSeed); // ปลูกพืชที่เลือก
        }
    }

    public void PlantSeed(GameObject seedPrefab)
    {
        if (state == TileState.Empty && seedPrefab != null)
        {
            if (currentPlant != null) Destroy(currentPlant); // ลบพืชเก่าถ้ามี
            currentPlant = Instantiate(seedPrefab, transform.position, Quaternion.identity, transform);
            state = TileState.Planted;
            Debug.Log($"✅ ปลูก {seedPrefab.name} แล้ว!");
        }
    }

    public void Harvest()
    {
        if (state == TileState.ReadyToHarvest && currentPlant != null)
        {
            string plantName = currentPlant.name.Replace("(Clone)", "").Trim(); // ดึงชื่อพืช

            InventoryManager.Instance.AddItem(plantName, 1); // เพิ่มไอเทมลงใน Inventory
            Destroy(currentPlant); // ลบพืชออกจากฟาร์ม
            state = TileState.Empty;

            Debug.Log($"🌾 เก็บเกี่ยว {plantName} แล้ว!");
        }
    }


    public void SetPlantVisibility(bool isVisible)
    {
        if (currentPlant != null)
        {
            currentPlant.SetActive(isVisible);
        }
    }
}
