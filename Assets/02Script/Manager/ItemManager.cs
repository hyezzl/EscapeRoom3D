using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Pickable,
    Interactable,
    Readable,
    Openable
}

public class ItemManager : Singleton<ItemManager>
{
    // 플레이어가 현재 바라보고있는 오브젝트
    public static IActionItem currentItem;

    public static void SetCurrentItem(IActionItem item) {
        currentItem = item;
    }
}
