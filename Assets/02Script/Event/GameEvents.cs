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

    // 게임모드(PlayerController) 변경
    public struct GameModeChange { }

    public struct EquipItem
    {
        public int itemID;
        public EquipItem(int itemID) { 
            this.itemID = itemID;
        }
    }

    public struct UnequipItem { }

    public struct OpenClockMode { }

    public struct DrawerUnlock { } // 서랍내에 숨은 아이템 활성화

    public struct LoadGameScene { } // 게임이 처음 시작
    
}
