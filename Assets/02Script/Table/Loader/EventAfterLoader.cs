using System.Collections.Generic;
using UnityEngine;

public class EventAfterData
{ 
    public string eventID;
    public string eventInfo;
    public string dialog;

}

public class EventAfterLoader
{
    private Dictionary<string, EventAfterData> eventafterDict = new();

    public EventAfterLoader(List<EventAfterEntity> table) {
        foreach (var row in table) {
            var data = new EventAfterData
            {
                eventID = row.EventID,
                eventInfo = row.EventInfo,
                dialog = row.Dialog
            };
            eventafterDict.Add(data.eventID, data);
        }
    }

    public EventAfterData Get(string eventID) {
        if (eventafterDict.TryGetValue(eventID, out var data)) {
            return new EventAfterData
            {
                eventID = data.eventID,
                eventInfo = data.eventInfo,
                dialog = data.dialog
            };
        }
        return null;
    }
}
