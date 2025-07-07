using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableItem : MonoBehaviour, IInspectable
{
    public int itemID;
    private InspectableData data;


    ItemType IActionItem.GetType() => data.type;

    private void Start()
    {
        data = ItemDatabaseManager.Instance.GetInspectable(itemID);
        if (data == null)
            Debug.Log($"InspectableItem - Failed to Load {itemID} Data");
    }
    public void InteractOnClick()
    {
        // Monologue Àç»ý
    }

    public void InteractOnE() { }
}
