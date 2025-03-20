using UnityEngine;
using System.Collections;

public class PlantGrowth : MonoBehaviour
{
    public Sprite[] growthStages; // เก็บ Sprite แต่ละช่วงของการเติบโต
    private SpriteRenderer spriteRenderer;
    private int currentStage = 0;
    private FarmTile parentTile;

    public float growthTimePerStage = 5f; // เวลาที่ใช้เติบโตต่อ 1 Stage
    private float growthTimer = 0f; // ตัวนับเวลา

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentTile = transform.parent?.GetComponent<FarmTile>(); // หาค่าจาก Tile ที่ปลูก

        Debug.Log($"🌱 พืชถูกปลูกที่ตำแหน่ง {transform.position}");
        StartCoroutine(GrowPlant());
    }

    IEnumerator GrowPlant()
    {
        while (currentStage < growthStages.Length)
        {
            spriteRenderer.sprite = growthStages[currentStage];
            growthTimer = growthTimePerStage; // ตั้งค่าเวลารอแต่ละช่วง

            while (growthTimer > 0)
            {
                Debug.Log($"⏳ พืชกำลังเติบโต... เหลือเวลา {Mathf.Ceil(growthTimer)} วินาที (Stage {currentStage + 1}/{growthStages.Length})");
                yield return new WaitForSeconds(1f);
                growthTimer -= 1f;
            }

            currentStage++;
        }

        // เมื่อพืชโตเต็มที่
        Debug.Log($"🌾 พืชโตเต็มที่แล้ว! พร้อมเก็บเกี่ยวที่ตำแหน่ง {transform.position}");

        if (parentTile != null)
        {
            parentTile.state = FarmTile.TileState.ReadyToHarvest;
        }
    }

    // เช็คว่าพืชโตเต็มที่หรือยัง
    public bool IsFullyGrown()
    {
        return currentStage >= growthStages.Length - 1;
    }

    // ดึงเวลาที่เหลือก่อนพืชจะเติบโตขึ้นไปอีกระดับ
    public float GetRemainingGrowthTime()
    {
        return growthTimer;
    }
}
