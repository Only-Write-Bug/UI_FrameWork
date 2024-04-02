using System;

namespace Tools.EventSystemTool
{
    public static class EventSystemTool
    {
        private static EventSystem _eventSystem = null;
        
        public static void Awake()
        {
            _eventSystem = EventSystem.Init;
        }

        public static int RegisterEvent(string eventName, Action<object[]> callback)
        {
            return _eventSystem.RegisterEvent(eventName, callback);
        }

        public static void PublishEvent(string eventName)
        {
            _eventSystem.PublishEvent(eventName);
        }

        public static void ClearEventAction(string eventName)
        {
            _eventSystem.ClearEventAction(eventName);
        }

        public static void RemoveAction(string eventName, int actionID)
        {
            _eventSystem.RemoveAction(eventName, actionID);
        }
    }
}