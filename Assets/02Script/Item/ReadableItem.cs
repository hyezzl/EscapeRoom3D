using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadableItem : MonoBehaviour, IReadable
{
    public int itemID;
    private ReadableData data;

    ItemType IActionItem.GetType() => data.type;

    private void Start()
    {
        data = ItemDatabaseManager.Instance.GetReadable(itemID);
        if (data == null)
            Debug.Log($"ReadableItem - Failed to Load {itemID} Data");
    }
    public void PressEMessage()
    {

    }

    public void DisplayDialog() {
        Debug.Log($"내용 : {data.dialog}");
    }

    public void InteractOnClick()
    {
        // Monologue 재생
    }

    public void InteractOnE()
    {
        // Dialog 재생

        // Reply 재생
    }
}
