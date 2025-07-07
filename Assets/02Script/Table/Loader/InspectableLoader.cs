using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectableData
{
    public int itemID;
    public string itemName;
    public ItemType type;
    public string monologue;
}

public class InspectableLoader
{
    private Dictionary<int, InspectableData> inspectableDict = new();

    public InspectableLoader(List<InspectableEntity> table) {
        foreach (var row in table)
        {
            ItemType itemtype;
            if (!Enum.TryParse(row.Type, out itemtype))
            {
                itemtype = ItemType.Inspectable;
            }

            var data = new InspectableData
            {
                itemID = row.ItemID,
                itemName = row.ItemName,
                type = itemtype,
                monologue = row.Monologue,
            };
            inspectableDict.Add(row.ItemID, data);
        }
    }

    public InspectableData Get(int itemID) {
        if (inspectableDict.TryGetValue(itemID, out var data)) {
            return new InspectableData
            {
                itemID = itemID,
                itemName = data.itemName,
                type = data.type,
                monologue = data.monologue
            };
        }
        return null;
    }
}
