using System;
using System.Collections.Generic;

namespace Tools.EventSystemTool
{
    public delegate void _eventHandle(params object[] args);
    
    public class EventModel
    {
        public string eventName;
        private Dictionary<int, EventActionShell> _actionsDic = new Dictionary<int, EventActionShell>();
        private List<EventActionShell> _actionShellsList = new List<EventActionShell>();

        public int AddAction(Action<object[]> callback)
        {
            var shell = new EventActionShell();
            _actionsDic.TryAdd(shell.get_actionID, shell);
            shell.callback = new _eventHandle(args => {callback?.Invoke(args);}) ;
            _actionShellsList.Add(shell);
            return shell.get_actionID;
        }

        public void Publish(params object[] args)
        {
            foreach (var shell in _actionShellsList)
            {
                shell.callback?.Invoke(args);
            }
        }

        public void RemoveAction(int actionID)
        {
            if (_actionsDic.TryGetValue(actionID, out var action))
                _actionShellsList.Remove(action);
            _actionsDic.Remove(actionID);
        }

        public void Dispose()
        {
            eventName = null;
            _actionsDic.Clear();
            _actionShellsList.Clear();
        }
    }

    public class EventActionShell
    {
        private static int actionID = 0;
        public EventActionShell() => _actionID = actionID++;
        private int _actionID;
        public int get_actionID => _actionID;
        
        public _eventHandle callback;
    }
}