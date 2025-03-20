using UnityEngine;
using System.Collections.Generic;

public class PuzzleGridManager : MonoBehaviour
{
    public int width = 6;
    public int height = 6;
    public float tileSize = 5f;
    public GameObject[] blockPrefabs;
    public PuzzleBlock selectedBlock = null;

    private GameObject[,] grid;
    public GameObject tilesContainer;
    public PanelManager panelManager;

    private void Start()
    {
        if (blockPrefabs == null || blockPrefabs.Length == 0)
        {
            Debug.LogError("blockPrefabs ไม่มีค่า ตรวจสอบว่าใส่ Prefabs ใน Inspector แล้วหรือยัง");
            return;
        }

        GameObject parentObj = GameObject.Find("TilesContain");
        if (parentObj == null)
        {
            Debug.LogError("ไม่พบ TilesContain ใน Hierarchy");
            return;
        }

        Transform parent = parentObj.transform;
        grid = new GameObject[width, height];
        GenerateGrid(parent);

        if (tilesContainer == null)
        {
            tilesContainer = GameObject.Find("TilesContain");
        }

        if (tilesContainer != null)
        {
            tilesContainer.SetActive(false);
        }
    }

    private void GenerateGrid(Transform parent)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 position = GetWorldPosition(x, y);
                GameObject block = GetRandomBlock();
                if (block == null)
                {
                    Debug.LogError("GetRandomBlock() คืนค่า null");
                    continue;
                }

                GameObject newBlock = Instantiate(block, parent);
                newBlock.transform.localPosition = position;

                grid[x, y] = newBlock;
            }
        }
    }

    private GameObject GetRandomBlock()
    {
        if (blockPrefabs.Length == 0) return null;
        return blockPrefabs[Random.Range(0, blockPrefabs.Length)];
    }

    public void TogglePuzzleGrid()
    {
        if (tilesContainer != null)
        {
            bool isActive = tilesContainer.activeSelf;
            tilesContainer.SetActive(!isActive); // สลับสถานะ เปิด-ปิด
        }
    }

    public bool GetBlockPosition(PuzzleBlock block, out int x, out int y)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (grid[i, j] == block.gameObject)
                {
                    x = i;
                    y = j;
                    return true;
                }
            }
        }
        x = -1;
        y = -1;
        return false;
    }

    public PuzzleBlock GetBlockAt(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return null;
        return grid[x, y]?.GetComponent<PuzzleBlock>();
    }

    public void SwapBlocks(PuzzleBlock block1, PuzzleBlock block2)
    {
        if (block1 == null || block2 == null) return;

        Vector3 tempPos = block1.transform.localPosition;
        block1.transform.localPosition = block2.transform.localPosition;
        block2.transform.localPosition = tempPos;

        int x1, y1, x2, y2;
        if (GetBlockPosition(block1, out x1, out y1) && GetBlockPosition(block2, out x2, out y2))
        {
            grid[x1, y1] = block2.gameObject;
            grid[x2, y2] = block1.gameObject;
        }

        CheckMatches();
    }

    private void CheckMatches()
    {
        List<GameObject> matchedBlocks = new List<GameObject>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject block = grid[x, y];
                if (block == null) continue;

                PuzzleBlock blockComponent = block.GetComponent<PuzzleBlock>();
                if (blockComponent == null) continue;

                List<GameObject> horizontalMatches = GetMatchingBlocks(x, y, 1, 0);
                List<GameObject> verticalMatches = GetMatchingBlocks(x, y, 0, 1);

                if (horizontalMatches.Count >= 3)
                {
                    matchedBlocks.AddRange(horizontalMatches);
                }
                if (verticalMatches.Count >= 3)
                {
                    matchedBlocks.AddRange(verticalMatches);
                }
            }
        }

        foreach (GameObject block in matchedBlocks)
        {
            Destroy(block);
        }

        CheckAndFillEmptySpaces();
    }

    private List<GameObject> GetMatchingBlocks(int startX, int startY, int dirX, int dirY)
    {
        List<GameObject> matches = new List<GameObject>();
        GameObject startBlock = grid[startX, startY];
        if (startBlock == null) return matches;

        matches.Add(startBlock);

        int x = startX + dirX;
        int y = startY + dirY;

        while (x >= 0 && x < width && y >= 0 && y < height)
        {
            GameObject nextBlock = grid[x, y];
            if (nextBlock == null || nextBlock.name != startBlock.name) break;

            matches.Add(nextBlock);
            x += dirX;
            y += dirY;
        }

        return matches;
    }

    public void CheckAndFillEmptySpaces()
    {
        Debug.Log("🔄 Checking and filling empty spaces...");

        bool hasEmptySpace = false;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] == null) // ถ้าตำแหน่งนี้ว่าง
                {
                    hasEmptySpace = true; // พบช่องว่าง
                    GameObject newBlockPrefab = GetRandomBlock();
                    if (newBlockPrefab != null)
                    {
                        GameObject newBlock = Instantiate(newBlockPrefab, tilesContainer.transform);
                        newBlock.transform.localPosition = GetWorldPosition(x, y);
                        grid[x, y] = newBlock;
                        Debug.Log($"✅ Filled empty space at ({x}, {y})");
                    }
                    else
                    {
                        Debug.LogError("❌ ไม่มี Prefab ให้ใช้สำหรับเติมบล็อก!");
                    }
                }
            }
        }
    }



    private Vector2 GetWorldPosition(int x, int y)
    {
        float startX = -((width - 1) * tileSize) / 2f;
        float startY = -((height - 1) * tileSize) / 2f;
        return new Vector2(startX + x * tileSize, startY + y * tileSize);
    }

    public void ClosePuzzlePanel()
    {
        if (tilesContainer != null)
        {
            foreach (Transform child in tilesContainer.transform)
            {
                child.gameObject.SetActive(false);  // ปิดบล็อกทั้งหมด
            }

            tilesContainer.SetActive(false);  // ปิด Panel
            Debug.Log("🛑 Puzzle Panel ถูกปิด และบล็อกทั้งหมดถูกซ่อน");
        }
    }

}
