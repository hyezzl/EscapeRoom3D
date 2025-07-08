using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    [Header("UI Refs")]
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;

    private const int minSlotCnt = 16;
    private List<InventoryUISlot> slots = new();

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<UIEvents.InventoryChanged>(OnInventoryChanged);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UIEvents.InventoryChanged>(OnInventoryChanged);
    }

    private void OnInventoryChanged(UIEvents.InventoryChanged evt) {
        RefreshInventory();
    }

    public void RefreshInventory() {
        var items = InventoryManager.Instance.GetInventory();
        ////////////////////
        
    }
}
