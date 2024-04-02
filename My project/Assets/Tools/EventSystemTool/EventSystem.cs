using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools.EventSystemTool
{
    public class EventSystem
    {
        private static EventSystem _init = null;
        public static EventSystem Init => _init ??= new EventSystem();

        private EventSystem()
        {
        }

        private Dictionary<string, EventModel> _eventContrainer = new Dictionary<string, EventModel>(8);
        
        public int RegisterEvent(string eventName, Action<object[]> callback)
        {
            _eventContrainer.TryAdd(eventName, new EventModel());
            return _eventContrainer[eventName].AddAction(callback);
        }

        public void PublishEvent(string eventName)
        {
            if(_eventContrainer.ContainsKey(eventName))
                _eventContrainer[eventName].Publish();
        }

        public void ClearEventAction(string eventName)
        {
            if (!_eventContrainer.TryGetValue(eventName, out EventModel curEvent))
                return;
            curEvent.Dispose();
        }

        public void RemoveAction(string eventName, int actionID)
        {
            if (!_eventContrainer.TryGetValue(eventName, out var curEvent))
                return;
            curEvent.RemoveAction(actionID);
        }
    }
}