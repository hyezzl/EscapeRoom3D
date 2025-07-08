using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Refs")]
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject selectBox;

    private ItemInstance itemInst;


    public void Set(ItemInstance newInst) { 
        itemInst = newInst;
        iconImage.sprite = itemInst.Data.icon;
        SetSelect(false);
    }

    private void SetSelect(bool isSelected) { 
        selectBox?.SetActive(isSelected);
    }

    public void OnPointerClick(PointerEventData evt)
    {
        EventBus.Instance.Publish<UIEvents.SlotClicked>(new UIEvents.SlotClicked(itemInst, this));
    }
}
