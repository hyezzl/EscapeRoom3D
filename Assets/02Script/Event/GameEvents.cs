using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    // 아이템 획득
    public struct GetItem
    {
        public PickableItem item;
        public GetItem(PickableItem item) {
            this.item = item;
        }
    }

    // 인벤토리 변경
    public struct InventoryChanged { }

    public struct GameModeChange { }
}
