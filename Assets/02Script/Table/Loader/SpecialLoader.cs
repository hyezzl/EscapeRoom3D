using System;
using System.Collections.Generic;

public class SpecialData
{
    public int itemID;
    public string itemName;
    public ItemType type;
    public string monologue;
    public int eventType;
}

public class SpecialLoader
{
    private Dictionary<int, SpecialData> specialDict = new();

    public SpecialLoader(List<SpecialEntity> table) {
        foreach (var row in table) {
            ItemType itemtype;
            if (!Enum.TryParse(row.Type, out itemtype))
            {
                itemtype = ItemType.Special;
            }

            var data = new SpecialData
            {
                itemID = row.ItemID,
                itemName = row.ItemName,
                type = itemtype,
                monologue = row.Monologue,
                eventType = row.EventType
            };
            specialDict.Add(row.ItemID, data);
        }
    }

    public SpecialData Get(int itemID) {
        if (specialDict.TryGetValue(itemID, out var data)) {
            return new SpecialData
            {
                itemID = itemID,
                itemName = data.itemName,
                type = data.type,
                monologue = data.monologue,
                eventType = data.eventType
            };
        }
        return null;
    }
}
