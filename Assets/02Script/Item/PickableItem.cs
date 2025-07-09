using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Outline))]
public class PickableItem : MonoBehaviour, IPickable
{
    public int itemID;
    private PickableData data;
    private PlayerSight sight;

    public int GetItemID() => itemID; // 이미 public인데?
    ItemType IActionItem.GetType() => data.type;
    public int GetPairID() => data.pairID; ////////////

    private void Start()
    {
        data = ItemDatabaseManager.Instance.GetPickable(itemID);
        if (data == null)
            Debug.Log($"PickableItem - Failed to Load {itemID} Data");

        if (TryGetComponent<Rigidbody>(out Rigidbody rig)){
            rig.useGravity = false;
        }
        if (TryGetComponent<Outline>(out Outline outline)) {
            outline.color = 1;
        }
    }

    public void OnpickUp()
    {
        Debug.Log("줍기행동");
        if (ItemManager.CurrentItem != null)
        {
            // 인벤토리 습득 (아니면 이벤트로?)
            //InventoryManager.Instance.AddItem(ItemManager.CurrentItem.GetItemID());
            // 이벤트
            EventBus.Instance.Publish<GameEvents.GetItem>(new GameEvents.GetItem(this));
        }
        else
            Debug.Log("PickableItem - CurrentItem is null");    
        ItemManager.CurrentItem = null;
        Destroy(gameObject);
    }

    public void InteractOnClick()
    {
        OnpickUp();
    }

    public void InteractOnE() { Debug.Log("E 기능없음"); }
}
