using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour {
    [Serializable]
    public class StoreItem {
        public ItemData data;
        public Button button;
    }

    #region Variable
    public List<StoreItem> StoreItems = new List<StoreItem>();

    private readonly string ADD_SLOT_BUTTON = "ADD_SLOT_BUTTON";
    private Button addSlotButton = null;

    private Inventory inventory = null;
    #endregion

    #region Life Cycle
    private void Awake() {
        Caching();
    }

    private void Start() {
        AddButtonsEvent();
    }
    #endregion

    #region Essential Function
    private void Caching() {
        try {
            addSlotButton = null ?? transform.Find(ADD_SLOT_BUTTON).GetComponent<Button>();
            inventory = null ?? FindObjectOfType<Inventory>();
        } catch (Exception e) {
            Debug.LogError(e);
        }
    }

    private void AddButtonsEvent() {
        foreach (var item in StoreItems)
            item.button.onClick.AddListener(() => PurchaseAnItem(item.data));

        addSlotButton.onClick.AddListener(() => { inventory.AddEmptySlot(); });
    }
    #endregion

    #region Definition Function
    private void PurchaseAnItem(ItemData itemData) {
        inventory.PurchaseItem(itemData);
    }
    #endregion
}
