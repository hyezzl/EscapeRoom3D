using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectData
{
    public int itemID;
    public string itemName;
    public string description;
    public string monologue;
    public Sprite icon;
    public ItemType type;
    public int pairID;
    public string dialog;
}

public class ObjectDataLoader
{
    private Dictionary<int, ObjectData> objectDict = new();

    public ObjectDataLoader(List<ObjectEntity> table) {
        foreach (var row in table) {
            // enum 변환
            ItemType itemType;
            if (!Enum.TryParse(row.Type, out itemType))
                itemType = ItemType.Interactable; // 변환실패 시 기본값

            var data = new ObjectData {
                itemID = row.ItemID,
                itemName = row.ItemName,
                description = row.Description,
                monologue = row.Monologue,
                icon = Resources.Load<Sprite>($"Icons/{row.IconName}"),
                type = itemType,
                pairID = row.PairID,
                dialog = row.Dialog
            };
            objectDict.Add(row.ItemID, data);
        }
    }

    public ObjectData Get(int itemID) { 
        if(objectDict.TryGetValue(itemID, out var data))
            return new ObjectData
            {
                itemID = itemID,
                itemName = data.itemName,
                description = data.description,
                monologue = data.monologue,
                icon = data.icon,
                type = data.type,
                pairID = data.pairID,
                dialog = data.dialog
            };
        return null;
    }
}
