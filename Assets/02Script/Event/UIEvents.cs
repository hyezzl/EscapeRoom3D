using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIEvents
{
    // Inventory
    public struct ToggleInventory { }

    public struct SlotClicked {
        public ItemInstance itemInst;
        public InventoryUISlot slot;

        public SlotClicked(ItemInstance itemInst, InventoryUISlot slot) { 
            this.itemInst = itemInst;
            this.slot = slot;
        }
    }

    // Dialog
    public struct OpenDialogPopup {
        public IActionItem item;
        public bool isClick;

        public OpenDialogPopup(IActionItem item, bool isClick) {
            this.item = item;
            this.isClick = isClick;
        }
    }

    public struct OpenNarrativePopup
    {
        public IActionItem item;

        public OpenNarrativePopup(IActionItem item) { 
            this.item= item;
        }
    }

    public struct OpenViewMode 
    {
        public int itemID;

        public OpenViewMode(int itemID) {
            this.itemID = itemID;
        }
    }

    public struct CloseViewMode { }

    public struct EventAfter {
        public string eventID;

        public EventAfter(string eventID) { 
            this.eventID = eventID;
        }
    }


    // ¿œ»∏º∫ //

    public struct StartTutorial { }
}
