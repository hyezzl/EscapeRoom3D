using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableData
{
    public int itemID;
    public string itemName;
    public ItemType type;
    public string monologue;
    public string deactiveMSG;
    public int pairID;

}

public class InteractableLoader
{
    private Dictionary<int, InteractableData> interactableDict = new();

    public InteractableLoader(List<InteractableEntity> table)
    {
        foreach (var row in table)
        {
            ItemType itemtype;
            if (!Enum.TryParse(row.Type, out itemtype))
            {
                itemtype = ItemType.Interactable;
            }

            var data = new InteractableData
            {
                itemID = row.ItemID,
                itemName = row.ItemName,
                type = itemtype,
                monologue = row.Monologue,
                deactiveMSG = row.DeactiveMSG,
                pairID = row.PairID
            };
            interactableDict.Add(row.ItemID, data);
        }
    }

    public InteractableData Get(int itemID)
    {
        if (interactableDict.TryGetValue(itemID, out var data))
            return new InteractableData
            {
                itemID = itemID,
                itemName = data.itemName,
                type = data.type,
                monologue = data.monologue,
                deactiveMSG = data.deactiveMSG,
                pairID = data.pairID
            };
        return null;
    }
}
