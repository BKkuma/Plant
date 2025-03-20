using UnityEngine;

public class FarmManager : MonoBehaviour
{
    private FarmTile[] farmTiles;

    void Start()
    {
        // ค้นหา FarmTile ที่มีอยู่แล้วใน Scene และเก็บไว้ใน farmTiles
        farmTiles = FindObjectsOfType<FarmTile>();
    }

    // ✅ เปิด/ปิด Grid ฟาร์ม พร้อมแสดง/ซ่อนพืชที่ปลูก
    public void SetFarmGridActive(bool active)
    {
        gameObject.SetActive(active);
        SetPlantVisibility(active);
    }

    // ✅ ซ่อน/แสดงพืชทั้งหมดในฟาร์ม
    private void SetPlantVisibility(bool visible)
    {
        if (farmTiles != null)
        {
            foreach (FarmTile tile in farmTiles)
            {
                tile.SetPlantVisibility(visible);
            }
        }
    }
}
