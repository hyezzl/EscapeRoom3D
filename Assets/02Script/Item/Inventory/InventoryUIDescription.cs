using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUIDescription : MonoBehaviour
{
    [Header("UI Refs")]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDesc;

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<UIEvents.SlotClicked>(OnSlotClicked);
        Clear();
    }

    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<UIEvents.SlotClicked>(OnSlotClicked);
    }

    private void OnSlotClicked(UIEvents.SlotClicked evt) {
        Debug.Log("½½·Ô Å¬¸¯!");
        ShowInform(evt.itemInst);
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
