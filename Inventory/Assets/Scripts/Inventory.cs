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
    /// �κ��丮 ���� ���ο� ������ ������ �߰��Ѵ�.
    /// </summary>
    public void AddEmptySlot() {
        GameObject inst = Instantiate(inventorySlotObject);
        inst.transform.SetParent(Content.transform, false);

        inventorySlots.Add(inst.GetComponent<InventorySlot>());
    }

    /// <summary>
    /// ������ ���� �� �κ��丮�� �߰��Ѵ�.
    /// </summary>
    /// <param name="itemData"></param>
    public void PurchaseItem(ItemData itemData) {
        // 1. �������� ������ Ÿ���� ���.
        if (CheckQuantityItem(itemData)) {
            // 1-1. �κ��丮�� ���� ������ ���� �˻�.
            InventorySlot slot = GetSameItemSlotInInventory(itemData);

            // 1-1-1. ���� ������ ������ �����ϸ� ������ ������Ŵ���ν� ȹ��.
            if (slot != null) slot.IncreaseQuantity();
            // 1-1-2. ���� ������ ������ �������� �ʴ´ٸ� ���Կ� ���� �߰���Ŵ���ν� ȹ��.
            else AddNewItem(itemData);
        }
        // 2. �������� �ʴ� ������ Ÿ���� ���.
        else AddNewItem(itemData);
    }

    /// <summary>
    /// �κ��丮 �ȿ� ����ִ� ������ ���Կ� �������� �߰��Ѵ�.
    /// </summary>
    /// <param name="itemData"></param>
    private void AddNewItem(ItemData itemData) {
        var slot = FindEmptySlot();
        if (slot == null) Debug.Log("����ִ� �κ��丮 ������ �������� �ʽ��ϴ�.");
        else slot.UpdateData(itemData);
    }

    /// <summary>
    /// �κ��丮 �ȿ� ����ִ� ������ ������ ã�� ��ȯ�Ѵ�.
    /// </summary>
    /// <returns>������ InventorySlot, ������ null</returns>
    private InventorySlot FindEmptySlot() {
        foreach (InventorySlot slot in inventorySlots) {
            if (slot.itemData.type == ItemData.eType.None)
                return slot;
        }

        return null;
    }

    /// <summary>
    /// �������� ���� Ÿ���� ���������� Ȯ���ϰ� boolean ��ȯ
    /// </summary>
    /// <param name="itemData">Ȯ���Ϸ��� ItemData</param>
    /// <returns>�������� �������̶�� false, �������� �ʴ� �������̶�� true</returns>
    private bool CheckQuantityItem(ItemData itemData) {
        if (itemData.type == ItemData.eType.Consumable ||
            itemData.type == ItemData.eType.Material) {
            return true;
        }
        return false;
    }

    /// <summary>
    /// ���� �������� �κ��丮�� �����ϴ��� ã�� ��ȯ.
    /// </summary>
    /// <param name="itemData">Ȯ���Ϸ��� ItemData</param>
    /// <returns>������ InventorySlot, ������ null</returns>
    private InventorySlot GetSameItemSlotInInventory(ItemData itemData) {
        foreach (var slot in inventorySlots) {
            if (slot.itemData.name == itemData.name && slot.itemData.count < MAX_COUNT)
                return slot;
        }

        return null;
    }
    #endregion
}