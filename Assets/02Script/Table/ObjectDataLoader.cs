using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { 
    Pickable,
    Interactive,
    Readable
}

public class ObjectData
{
    public int itemID;
    public string itemName;
    public string description;
    public string monologue;
    public string iconName;
    public ItemType type;
    public int pairID;
    public DialogueData dialogData;
}

public class ObjectDataLoader
{
    private Dictionary<int, ObjectData> objectDict = new();

    public ObjectDataLoader(List<ObjectEntity> table, DialogueDataLoader dialogLoader) {
        foreach (var row in table) {
            // enum 변환
            ItemType itemType;
            if (!Enum.TryParse(row.Type, out itemType))
                itemType = ItemType.Interactive; // 변환실패 시 기본값

            var data = new ObjectData {
                itemID = row.ItemID,
                itemName = row.ItemName,
                description = row.Description,
                monologue = row.Monologue,
                iconName = row.IconName,
                type = itemType,
                pairID = row.PairID,
                dialogData = dialogLoader.Get(row.DialogID)
            };
            objectDict.Add(row.ItemID, data);
        }
    }

    public ObjectData Get(int itemID) { 
        objectDict.TryGetValue(itemID, out var data);
        //if (data == null) Debug.Log("ObjectLoader - Get Error");
        return data;
    }
}
