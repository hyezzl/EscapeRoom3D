using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    public int itemID;
    private ObjectData data;

    private void Start()
    {
        data = ItemDatabaseManager.Instance.GetData(itemID);
        if (data == null)
            Debug.Log($"Failed to Load {itemID} Data");
    }


    public void PlayMonologue()
    {
        Debug.Log($"독백 재생 : {data.monologue}");
    }

    ItemType IActionItem.GetType()
    {
        return data.type;
    }
}
