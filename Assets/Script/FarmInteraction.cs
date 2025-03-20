using UnityEngine;

public class FarmInteraction : MonoBehaviour
{
    public GameObject seedPrefab; // เมล็ดพืชที่ต้องการปลูก

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // คลิกซ้าย
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                FarmTile tile = hit.collider.GetComponent<FarmTile>();
                if (tile != null)
                {
                    tile.PlantSeed(seedPrefab);
                }
            }
        }
    }


}
