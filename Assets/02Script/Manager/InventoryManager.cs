using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    private List<ItemInstance> inventory = new();


    // 시작 시 이미 가지고있을 아이템
    private void Start()
    {
        AddItem(10001006);
        AddItem(10001003); // 임시
    }

    private void OnEnable()
    {
        EventBus.Instance.Subscribe<GameEvents.GetItem>(OnGetItem);
    }
    private void OnDisable()
    {
        EventBus.Instance.Unsubscribe<GameEvents.GetItem>(OnGetItem);
    }

    private void OnGetItem(GameEvents.GetItem evt) {
        AddItem(evt.item.GetItemID());
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
