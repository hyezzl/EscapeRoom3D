using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    private List<ItemInstance> inventory = new();

    public void AddItem(int itemID) { 
        //var itemData = ItemDatabaseManager.Instance.GetData(itemID);
    }
}
