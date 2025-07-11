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

        public OpenDialogPopup(IActionItem item) {
            this.item = item;
        }
    }
}
