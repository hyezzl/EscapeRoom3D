using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus : Singleton<EventBus>
{
    private Dictionary<Type, Delegate> eventTable = new();

    public void Subscribe<T>(Action<T> callback) {
        var type = typeof(T);
        if (eventTable.TryGetValue(type, out var del))
        {
            eventTable[type] = Delegate.Combine(del, callback);
        }
        else
        {
            eventTable[type] = callback;
        }
    }

    public void Unsubscribe<T>(Action<T> callback)
    {
        var type = typeof(T);
        if (eventTable.TryGetValue(type, out var del)) {
            var currentDel = Delegate.Remove(del, callback);
            if(currentDel == null)
                eventTable.Remove(type);
            else
                eventTable[type] = currentDel;
        }
    }

    public void Publish<T>(T eventData) {
        var type = typeof(T);
        if(eventTable.TryGetValue(type, out var del)){ 
            var callback = del as Action<T>;
            callback?.Invoke(eventData);
        }
    }
}
