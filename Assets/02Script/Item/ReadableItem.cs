using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableItem : MonoBehaviour, IReadable
{
    public int itemID;
    private ObjectData data;

    private void Start()
    {
        data = ItemDatabaseManager.Instance.GetData(itemID);
        if (data == null)
            Debug.Log($"Failed to Load {itemID} Data");
    }
    public void PressEMessage()
    {

    }

    public void DisplayDialog() {
        Debug.Log($"³»¿ë : {data.dialog}");
    }

    ItemType IActionItem.GetType()
    {
        return data.type;
    }
}
