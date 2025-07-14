using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class InspectableItem : MonoBehaviour, IInspectable
{
    public int itemID;
    private InspectableData data;


    public int GetItemID() => itemID;
    ItemType IActionItem.GetType() => data.type;

    private void Start()
    {
        data = ItemDatabaseManager.Instance.GetInspectable(itemID);
        if (data == null)
            Debug.Log($"InspectableItem - Failed to Load {itemID} Data");
        if (TryGetComponent<Outline>(out Outline outline)) {
            outline.color = 1;
        }
    }

    private void PlayMonologue()
    {
        Debug.Log($"독백 : {data.monologue}");
    }

    public void InteractOnClick()
    {
        
        PlayMonologue();
        EventBus.Instance.Publish<UIEvents.OpenDialogPopup>(new UIEvents.OpenDialogPopup(ItemManager.CurrentItem, true));
    }

    public void InteractOnE() { Debug.Log("E 기능없음"); }
}
