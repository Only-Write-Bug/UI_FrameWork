using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Tools.TimeTaskTool.TimeTask
{
    public class ScheduledTask : TimeTaskBase
    {
        public ScheduledTask(int timeArgs, Action callback)
        {
            this.timeStepArgs = this.countDown = timeArgs;
            this.callback = callback;
            this.taskType = TIMETASKTYPE.SCHEDULEDTASK;
        }

        public override Task OnTick(int timeArgs)
        {
            base.OnTick(timeArgs);

            if (isPause || countDown > 0)
                return null;
            try
            {
                callback?.Invoke();
                TimeTaskManager.Init.Remove_TaskByTaskID(this.get_TaskID);
            }
            finally
            {
                _callbackLock.Release();
            }

            return null;
        }
    }
}