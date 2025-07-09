using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickableData
{
    public int itemID;
    public string itemName;
    public ItemType type;
    public string description;
    public Sprite icon;
    public int pairID;
}


public class PickableLoader
{
    private Dictionary<int, PickableData> pickableDict = new();

    public PickableLoader(List<PickableEntity> table) {
        foreach (var row in table) {
            ItemType itemtype;
            if (!Enum.TryParse(row.Type, out itemtype)) {
                itemtype = ItemType.Pickable;
            }

            var data = new PickableData
            {
                itemID = row.ItemID,
                itemName = row.ItemName,
                type = itemtype,
                description = row.Description,
                icon = Resources.Load<Sprite>($"Icons/{row.IconName}"),
                pairID = row.PairID
            };
            if(data.icon == null) Debug.Log($"{data.itemID} : {data.itemName} 의 아이콘이 없음!");
            pickableDict.Add(row.ItemID, data);
        }
    }

    public PickableData Get(int itemID) {
        if (pickableDict.TryGetValue(itemID, out var data))
            return new PickableData
            {
                itemID = itemID,
                itemName = data.itemName,
                type = data.type,
                description = data.description,
                icon = data.icon,
                pairID = data.pairID
            };
        return null;
    }
}
