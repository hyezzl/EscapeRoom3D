using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������
/// </summary>
[Serializable]
public class ItemInstance
{
    public int itemID;
    public int uniqueID; // ���� �ν��Ͻ� ���� ID

    public ItemInstance(int itemID) { 
        this.itemID = itemID;
        this.uniqueID = ItemUIDGenerator.Generate();
    }
}
