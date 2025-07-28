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

    // 아이템을 착용하고 문에 접근 (E)
    public struct KnockDoor
    {
        public int pairID;
        public KnockDoor(int pairID) { 
            this.pairID = pairID;
        }
    }

    public struct PlayTimeline 
    {
        public string timelineID;
        public PlayTimeline(string timelineID) { 
            this.timelineID = timelineID;
        }
    }

    public struct EndTimeline
    {
        public string timelineID;
        public EndTimeline(string timelineID) { 
            this.timelineID = timelineID;
        }
    }

    // 카메라 변경 (type == 바뀌는 카메라)
    public struct ChangeCam
    {
        public CameraType type;
        public ChangeCam(CameraType type) { 
            this.type = type;
        }
    }

    // 사용한 아이템 삭제
    public struct DestroyItem
    {
        public PickableData item;
        public DestroyItem(PickableData item) { 
            this.item = item;   
        }
    }



}
