using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    private List<ItemInstance> inventory = new();

    public void AddItem(int itemID) { 
        var itemData = ItemDatabaseManager.Instance.GetData(itemID);
        if (itemData == null) {
            Debug.Log($"{itemID} : unknown item ERROR");
            return;
        }
        inventory.Add(new ItemInstance(itemID));
    }

    public IReadOnlyList<ItemInstance> GetInventory() => inventory.AsReadOnly();

    // 세이브된 인벤토리 데이터 로드
    public void LoadItemInstance(ItemInstance newItemInst) { 
        inventory.Add(newItemInst);
    }
}
