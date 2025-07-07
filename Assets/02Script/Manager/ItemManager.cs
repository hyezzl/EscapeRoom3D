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
}
