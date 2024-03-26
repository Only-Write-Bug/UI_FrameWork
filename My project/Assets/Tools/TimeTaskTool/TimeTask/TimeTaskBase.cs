using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Tools.TimeTaskTool.TimeTask
{
    public class TimeTaskBase
    {
        private static int _taskID = 0;

        private int _childTaskID;
        public int get_TaskID => _childTaskID;

        public TimeTaskBase() => _childTaskID = _taskID++;

        protected int timeStepArgs = 0;
        protected int countDown = 0;

        protected TIMETASKTYPE taskType = TIMETASKTYPE.DEFAULT;

        protected SemaphoreSlim _callbackLock = new SemaphoreSlim(1);
        public bool isPause = false;

        public Action callback = null;
        protected int initial_Loop = 0;
        public int loop = 0;

        private bool isCallbackRunning = false;
        
        public virtual async Task OnTick(int timeArgs)
        {
            this.countDown -= timeArgs;
            await _callbackLock.WaitAsync();
        }

        public virtual void OnPause()
        {
            isPause = true;
        }

        public virtual void OnReset()
        {
            countDown = timeStepArgs;
        }

        public virtual void OnAwake()
        {
            isPause = false;
        }

        public virtual void OnDispose()
        {
            isPause = true;
            callback = null;
        }
    }
}