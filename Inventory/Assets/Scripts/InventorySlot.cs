using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {
    #region Variable
    public ItemData itemData { get; private set; } = new ItemData();

    private readonly string CONTENT = "CONTENT";
    private Image contentImage = null;
    private readonly string DRAG_IMAGE = "DRAG_IMAGE";
    private Image dragImage = null;
    private readonly string COUNT = "COUNT";
    private TMP_Text countText = null;

    private readonly string CANVAS = "CANVAS";
    private GameObject canvas = null;

    private bool isDragging = false;
    #endregion

    #region Life Cycle
    private void Awake() {
        Caching();
    }

    private void Start() {
        Init();
    }
    #endregion

    #region Essential Function
    private void Caching() {
        try {
            canvas = null ?? GameObject.Find(CANVAS);
            contentImage = null ?? transform.Find(CONTENT).GetComponent<Image>();
            dragImage = null ?? transform.Find(DRAG_IMAGE).GetComponent<Image>();
            countText = null ?? transform.Find(COUNT).GetComponent<TMP_Text>();
        } catch (Exception e) {
            Debug.LogError(e);
        }
    }

    private void Init() {
        dragImage.enabled = false;
        contentImage.color = Color.clear;
        countText.gameObject.SetActive(false);
    }
    #endregion

    #region Definition Function
    /// <summary>
    /// 아이템 데이터를 전달받아 데이터 업데이트
    /// </summary>
    /// <param name="itemData">업데이트 시킬 아이템 데이터</param>
    public void UpdateData(ItemData itemData) {
        this.itemData = itemData;

        contentImage.sprite = this.itemData.sprite;
        contentImage.color = itemData.type == ItemData.eType.None ? Color.clear : Color.white;

        if (itemData.type == ItemData.eType.Material ||
            itemData.type == ItemData.eType.Consumable) {
            countText.text = itemData.count.ToString();
            countText.gameObject.SetActive(true);
        } else {
            countText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 아이템 데이터 삭제 및 초기화
    /// </summary>
    /// <param name="inventorySlot"></param>
    private void DeleteData(InventorySlot inventorySlot) {
        inventorySlot.itemData = new ItemData();

        inventorySlot.contentImage.sprite = null;
        inventorySlot.contentImage.color = Color.clear;
    }

    /// <summary>
    /// 수량 증가로 합쳐져야하는 아이템의 경우 수량을 증가 시킴
    /// </summary>
    public void IncreaseQuantity() {
        ItemData itemData = new ItemData();
        itemData.type = this.itemData.type;
        itemData.name = this.itemData.name;
        itemData.description = this.itemData.description;
        itemData.count = this.itemData.count;
        itemData.sprite = this.itemData.sprite;
        itemData.count++;

        UpdateData(itemData);
    }

    /// <summary>
    /// 다른 아이템 슬롯과 교체
    /// </summary>
    public void SwapSlot(InventorySlot inventorySlot) {
        ItemData tempData = new ItemData();
        tempData = this.itemData;

        // 아이템 -> 빈 슬롯 이동
        if (inventorySlot.itemData.type == ItemData.eType.None) {
            inventorySlot.UpdateData(tempData);
            this.DeleteData(new InventorySlot());
        }
        // 아이템 -> 아이템 교체
        else {
            this.UpdateData(inventorySlot.itemData);
            inventorySlot.UpdateData(tempData);
        }
    }

    #endregion

    #region EventSystems
    public void OnBeginDrag(PointerEventData eventData) {
        var slot = eventData.pointerDrag.GetComponent<InventorySlot>();

        if (slot == null || slot.itemData.sprite == null) return;

        isDragging = true;
        contentImage.enabled = false;
        dragImage.sprite = slot.itemData.sprite;
        dragImage.enabled = true;
        dragImage.transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData) {
        if (!isDragging) return;
        dragImage.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        isDragging = false;
        contentImage.enabled = true;
        dragImage.enabled = false;
        dragImage.transform.SetParent(transform);
        dragImage.transform.position = Vector3.zero;
    }

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag == gameObject) return;

        var slot = eventData.pointerDrag.GetComponent<InventorySlot>();
        if (slot == null) return;
        SwapSlot(slot);
    }
    #endregion
}
