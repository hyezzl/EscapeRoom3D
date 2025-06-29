using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 얻은 아이템
/// </summary>
[Serializable]
public class ItemInstance
{
    public int itemID;
    public int uniqueID; // 개별 인스턴스 구별 ID

    public ItemInstance(int itemID) { 
        this.itemID = itemID;
        this.uniqueID = ItemUIDGenerator.Generate();
    }
}
