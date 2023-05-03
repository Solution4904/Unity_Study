using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    #region Variable
    private readonly string CONTENT = "CONTENT";
    private Transform Content = null;
    private readonly int MAX_COUNT = 10;

    [SerializeField] private GameObject inventorySlotObject = null;
    private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    #endregion

    #region Life Cycle
    private void Awake() {
        Caching();
    }
    #endregion

    #region Essential Function
    private void Caching() {
        try {
            Content = null ?? GetChildTransformByName(transform, CONTENT);
        } catch (Exception e) {
            Debug.LogError(e);
        }
    }
    #endregion

    #region Definition Function
    private Transform GetChildTransformByName(Transform parent, string name) {
        Transform[] transforms = parent.GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < transforms.Length; i++) {
            if (transforms[i].name == name) return transforms[i];
        }
        return null;
    }

    /// <summary>
    /// 인벤토리 내에 새로운 아이템 슬롯을 추가한다.
    /// </summary>
    public void AddEmptySlot() {
        GameObject inst = Instantiate(inventorySlotObject);
        inst.transform.SetParent(Content.transform, false);

        inventorySlots.Add(inst.GetComponent<InventorySlot>());
    }

    /// <summary>
    /// 아이템 구매 시 인벤토리에 추가한다.
    /// </summary>
    /// <param name="itemData"></param>
    public void PurchaseItem(ItemData itemData) {
        // 1. 겹쳐지는 아이템 타입일 경우.
        if (CheckQuantityItem(itemData)) {
            // 1-1. 인벤토리에 같은 아이템 슬롯 검색.
            InventorySlot slot = GetSameItemSlotInInventory(itemData);

            // 1-1-1. 같은 아이템 슬롯이 존재하면 수량을 증가시킴으로써 획득.
            if (slot != null) slot.IncreaseQuantity();
            // 1-1-2. 같은 아이템 슬롯이 존재하지 않는다면 슬롯에 새로 추가시킴으로써 획득.
            else AddNewItem(itemData);
        }
        // 2. 겹쳐지지 않는 아이템 타입일 경우.
        else AddNewItem(itemData);
    }

    /// <summary>
    /// 인벤토리 안에 비어있는 아이템 슬롯에 아이템을 추가한다.
    /// </summary>
    /// <param name="itemData"></param>
    private void AddNewItem(ItemData itemData) {
        var slot = FindEmptySlot();
        if (slot == null) Debug.Log("비어있는 인벤토리 슬롯이 존재하지 않습니다.");
        else slot.UpdateData(itemData);
    }

    /// <summary>
    /// 인벤토리 안에 비어있는 아이템 슬롯을 찾아 반환한다.
    /// </summary>
    /// <returns>있으면 InventorySlot, 없으면 null</returns>
    private InventorySlot FindEmptySlot() {
        foreach (InventorySlot slot in inventorySlots) {
            if (slot.itemData.type == ItemData.eType.None)
                return slot;
        }

        return null;
    }

    /// <summary>
    /// 겹쳐지는 수량 타입의 아이템인지 확인하고 boolean 반환
    /// </summary>
    /// <param name="itemData">확인하려는 ItemData</param>
    /// <returns>겹쳐지는 아이템이라면 false, 겹쳐지지 않는 아이템이라면 true</returns>
    private bool CheckQuantityItem(ItemData itemData) {
        if (itemData.type == ItemData.eType.Consumable ||
            itemData.type == ItemData.eType.Material) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 같은 아이템이 인벤토리에 존재하는지 찾아 반환.
    /// </summary>
    /// <param name="itemData">확인하려는 ItemData</param>
    /// <returns>있으면 InventorySlot, 없으면 null</returns>
    private InventorySlot GetSameItemSlotInInventory(ItemData itemData) {
        foreach (var slot in inventorySlots) {
            if (slot.itemData.name == itemData.name && slot.itemData.count < MAX_COUNT)
                return slot;
        }

        return null;
    }
    #endregion
}
