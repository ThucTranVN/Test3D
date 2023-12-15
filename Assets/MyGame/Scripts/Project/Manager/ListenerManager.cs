using System;
using System.Collections.Generic;

public class ListenerManager : BaseManager<ListenerManager>
{
    public Dictionary<ListenType, ListenerGroup> listeners = new Dictionary<ListenType, ListenerGroup>();

    public void BroadCast(ListenType type, object value = null)
    {
        if (listeners.ContainsKey(type) && listeners[type] != null)
        {
            listeners[type].BroadCast(value);
        }
    }

    public void Register(ListenType type, Action<object> action)
    {
        if (!listeners.ContainsKey(type))
        {
            listeners.Add(type, new ListenerGroup());
        }

        if (listeners[type] != null)
        {
            listeners[type].Attach(action);
        }
    }

    public void Unregister(ListenType type, Action<object> action)
    {
        if (listeners.ContainsKey(type) && listeners[type] != null)
        {
            listeners[type].Detach(action);
        }
    }

    public void UnregisterAll(Action<object> action)
    {
        foreach (ListenType key in listeners.Keys)
        {
            Unregister(key, action);
        }
    }
}