using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour, IPointerClickHandler
{
    [Header("UI Refs")]
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject selectBox;
    [SerializeField] private GameObject equipBox;

    private ItemInstance itemInst;
    public ItemInstance ItemInst => itemInst;

    public void Set(ItemInstance newInst) {
        if (newInst != null && newInst.Data != null)
        {
            itemInst = newInst;
            iconImage.enabled = true;
            iconImage.sprite = itemInst.Data.icon;
            if (itemInst.Data.icon == null)
                Debug.Log($"{itemInst.itemID} 의 아이콘 없음!!");
        }
        else { 
            iconImage.enabled = false;
            iconImage.sprite = null;
        }
        SetSelect(false);
    }

    // 선택 박스
    public void SetSelect(bool isSelected) { 
        selectBox?.SetActive(isSelected);
    }

    public void SetEquip(bool isEquip) { 
        equipBox?.SetActive(isEquip);
    }

    public void OnPointerClick(PointerEventData evt)
    {
        EventBus.Instance.Publish<UIEvents.SlotClicked>(new UIEvents.SlotClicked(itemInst, this));
    }
}
