using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UIEvents;

public class InventoryUIDescription : MonoBehaviour
{
    [Header("UI Refs")]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDesc;

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<UIEvents.SlotClicked>(OnSlotClicked);
        EventBus.Instance.Subscribe<UIEvents.ToggleInventory>(OnToggleInventory);
        Clear();
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UIEvents.SlotClicked>(OnSlotClicked);
        EventBus.Instance.Unsubscribe<UIEvents.ToggleInventory>(OnToggleInventory);
    }

    private void OnSlotClicked(UIEvents.SlotClicked evt) {
        ShowInform(evt.itemInst);
    }

    private void OnToggleInventory(UIEvents.ToggleInventory evt) {
        // ¿Œ∫•≈‰∏Æ ø©¥›¿ª ∂ß∏∂¥Ÿ ∫Ûƒ≠ √ ±‚»≠
        Clear();
    }

    // InformArea Clear
    public void Clear() {
        if (itemName == null || itemDesc == null) return;
        itemName.text = "";
        itemDesc.text = "";
    }

    public void ShowInform(ItemInstance itemInst) {
        if (itemInst == null) return;
        if (itemName == null || itemDesc == null) return;

        itemName.text = itemInst.Data.itemName;
        itemDesc.text = itemInst.Data.description;
    }
}
