using UnityEngine;

public class SeedSelector : MonoBehaviour
{
    public static GameObject selectedSeed = null;

    public GameObject pumpkinPrefab;
    public GameObject strawberryPrefab;
    public GameObject auberginePrefab;

    public GameObject seedUI; // ตัว UI ที่แสดงเมล็ดพันธุ์
    public GameObject farmPanel; // อ้างอิงไปที่ Farm Panel

    void Update()
    {
        if (farmPanel != null && seedUI != null)
        {
            seedUI.SetActive(farmPanel.activeSelf); // เปิด UI ก็ต่อเมื่อ Farm Panel เปิดอยู่
        }
    }

        public void SelectPumpkin()
    {
        selectedSeed = pumpkinPrefab;
        Debug.Log("🎃 เลือกเมล็ดฟักทอง");
    }

    public void SelectStrawberry()
    {
        selectedSeed = strawberryPrefab;
        Debug.Log("🍓 เลือกเมล็ดสตรอว์เบอร์รี");
    }

    public void SelectAubergine()
    {
        selectedSeed = auberginePrefab;
        Debug.Log("🍆 เลือกเมล็ดมะเขือ");
    }
}
