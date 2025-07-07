using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReadableData
{
    public int itemID;
    public string itemName;
    public ItemType type;
    public string monologue;
    public string dialog;
    public string reply;
}
public class ReadableLoader
{
    private Dictionary<int, ReadableData> readableDict = new();
    public ReadableLoader(List<ReadableEntity> table)
    {
        foreach (var row in table)
        {
            ItemType itemtype;
            if (!Enum.TryParse(row.Type, out itemtype))
            {
                itemtype = ItemType.Readable;
            }

            var data = new ReadableData
            {
                itemID = row.ItemID,
                itemName = row.ItemName,
                type = itemtype,
                monologue = row.Monologue,
                dialog = row.Dialog,
                reply = row.Reply
            };
            readableDict.Add(row.ItemID, data);
        }
    }

    public ReadableData Get(int itemID) {
        if (readableDict.TryGetValue(itemID, out var data)) {
            return new ReadableData
            {
                itemID = itemID,
                itemName = data.itemName,
                type = data.type,
                monologue = data.monologue,
                dialog = data.dialog,
                reply = data.reply
            };
        }
        return null;
    }
}
