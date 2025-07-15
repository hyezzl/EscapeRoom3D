using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Pickable,
    Interactable,
    Inspectable,
    Readable,
    Openable
}

public class ItemManager : Singleton<ItemManager>
{
    // 플레이어가 현재 바라보고있는 오브젝트
    private static IActionItem currentItem;
    public static IActionItem CurrentItem { get; set; }

    // 플레이어가 현재 장착하고있는 아이템
    private static PickableData equipItem;
    public static PickableData EquipItem { get; set; }

    // 인벤토리에서 선택된 슬롯 / 아이템
    private static InventoryUISlot selectedSlot;
    public static InventoryUISlot SelectedSlot { get; set; }
}
