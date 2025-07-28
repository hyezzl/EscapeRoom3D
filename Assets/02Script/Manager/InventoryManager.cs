using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    private List<ItemInstance> inventory = new();


    // 시작 시 이미 가지고있을 아이템
    private void Start()
    {
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.GetItem>(OnGetItem);
        EventBus.Instance.Subscribe<GameEvents.DestroyItem>(RemoveItem);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.GetItem>(OnGetItem);
        EventBus.Instance.Unsubscribe<GameEvents.DestroyItem>(RemoveItem);
    }

    private void OnGetItem(GameEvents.GetItem evt) {
        AddItem(evt.item.GetItemID());
    }

    private void RemoveItem(GameEvents.DestroyItem evt) {
        // todo :: itemUniqueID로 삭제하기
        ItemInstance removeItem = inventory.Find(item => item.itemID == evt.item.itemID);
        if (removeItem != null)
        {
            inventory.Remove(removeItem);
            EventBus.Instance.Publish<GameEvents.InventoryChanged>(new GameEvents.InventoryChanged());
        }
        else {
            Debug.Log("삭제할 아이템 없음");
        }
    }


    public void AddItem(int itemID) { 
        var itemData = ItemDatabaseManager.Instance.GetPickable(itemID);
        if (itemData == null) {
            Debug.Log($"{itemID} : unknown item ERROR");
            return;
        }
        inventory.Add(new ItemInstance(itemID));
        EventBus.Instance.Publish<GameEvents.InventoryChanged>(new GameEvents.InventoryChanged());
    }

    public IReadOnlyList<ItemInstance> GetInventory() => inventory.AsReadOnly();


    // 세이브된 인벤토리 데이터 로드
    public void LoadItemInstance(ItemInstance newItemInst) { 
        inventory.Add(newItemInst);
    }
}
