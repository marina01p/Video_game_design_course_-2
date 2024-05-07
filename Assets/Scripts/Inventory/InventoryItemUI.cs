using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item itemData;
    private CanvasGroup canvasGroup;
    private Transform originalParent;
    private Vector2 originalPosition;
    private Canvas canvas;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalParent = transform.parent;
        originalPosition = transform.position;
    }

    public void SetItemData(Item data)
    {
        itemData = data;
        GetComponent<Image>().sprite = itemData.icon;
        GetComponentInChildren<TextMeshProUGUI>().text = itemData.count.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (!eventData.pointerEnter)
        {
            InventoryManager.Instance.DropItemToWorld(itemData, eventData.position);
        }
        
        transform.position = originalPosition;
        transform.SetParent(originalParent);
        UpdateItemUI();
    }

    public void UpdateItemUI()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = itemData.count > 0 ? itemData.count.ToString() : "";
        this.gameObject.SetActive(itemData.count > 0);
    }

}
