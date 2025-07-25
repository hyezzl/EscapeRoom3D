using cakeslice;
using System.Collections;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Outline))]
public class PickableItem : MonoBehaviour, IPickable
{
    public int itemID;
    private PickableData data;

    public int GetItemID() => itemID;
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
        if (ItemManager.CurrentItem != null)
        {
            EventBus.Instance.Publish<GameEvents.GetItem>(new GameEvents.GetItem(this));
        }
        else
            Debug.Log("PickableItem - CurrentItem is null");    
        
        ItemManager.CurrentItem = null;
        StartCoroutine(SafeDestroy());
    }

    public void InteractOnClick()
    {
        OnpickUp();
    }

    public void InteractOnE() { Debug.Log("E 기능없음"); }

    private IEnumerator SafeDestroy() { 
        yield return null;
        Destroy(gameObject);
    }
}
