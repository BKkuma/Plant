using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PuzzleBlock : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 startPosition;
    private Vector2 dragDirection;
    private bool isDragging = false;
    private static PuzzleBlock selectedBlock;
    private PuzzleGridManager gridManager;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        gridManager = FindObjectOfType<PuzzleGridManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log("Clicked on: " + hit.collider.gameObject.name);
        }
        else
        {
            Debug.Log("Clicked on nothing");
        }

        if (selectedBlock != null)
        {
            selectedBlock.ResetHighlight();
        }

        selectedBlock = this;
        startPosition = transform.position;
        HighlightBlock();
        Debug.Log($"🟢 Block Selected: {gameObject.name} at {transform.position}");
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
        Vector2 dragPosition = eventData.position;
        dragDirection = (dragPosition - startPosition).normalized;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging)
        {
            Debug.Log($"🔄 Trying to Swap: {gameObject.name}");
            SwapBlock();
        }
        isDragging = false;
    }

    private void SwapBlock()
    {
        if (gridManager == null)
        {
            Debug.LogError("❌ Grid Manager Not Found!");
            return;
        }

        int x, y;
        if (!gridManager.GetBlockPosition(this, out x, out y))
        {
            Debug.LogError($"❌ Block Position Not Found: {gameObject.name}");
            return;
        }

        int targetX = x, targetY = y;

        if (Mathf.Abs(dragDirection.x) > Mathf.Abs(dragDirection.y))
        {
            targetX += dragDirection.x > 0 ? 1 : -1;
        }
        else
        {
            targetY += dragDirection.y > 0 ? 1 : -1;
        }

        Debug.Log($"🔄 Swapping {gameObject.name} ({x},{y}) with ({targetX},{targetY})");

        PuzzleBlock targetBlock = gridManager.GetBlockAt(targetX, targetY);
        if (targetBlock != null)
        {
            gridManager.SwapBlocks(this, targetBlock);
            ResetHighlight();
            targetBlock.ResetHighlight();

            // ตรวจสอบและเติมบล็อกหลังจากการสลับ
            gridManager.CheckAndFillEmptySpaces();
        }
    }

    public void OnClick()
    {
        Debug.Log("Clicked on " + gameObject.name);

        if (gridManager == null)
        {
            Debug.LogError("❌ gridManager is null in OnClick()");
            return;
        }

        if (gridManager.selectedBlock == null)
        {
            gridManager.selectedBlock = this;
            HighlightBlock();
            Debug.Log("Selected First Block: " + gameObject.name);
        }
        else
        {
            Debug.Log("Selected Second Block: " + gameObject.name);
            gridManager.SwapBlocks(gridManager.selectedBlock, this);
            gridManager.selectedBlock.ResetHighlight();
            gridManager.selectedBlock = null; // Reset selection
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);
                PuzzleBlock block = hit.collider.GetComponent<PuzzleBlock>();

                if (block != null)
                {
                    block.OnClick();
                }
            }
        }
    }

    private void HighlightBlock()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.gray;
        }
    }

    public void ResetHighlight()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    public void DestroyBlock()
    {
        StartCoroutine(DestroyAnimation());
    }

    private IEnumerator DestroyAnimation()
    {
        float duration = 0.3f;
        float elapsedTime = 0f;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
