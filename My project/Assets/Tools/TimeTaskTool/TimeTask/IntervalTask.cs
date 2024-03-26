using System;
using System.Threading.Tasks;

namespace Tools.TimeTaskTool.TimeTask
{
    public class IntervalTask : TimeTaskBase
    {
        public IntervalTask(int timeArgs, int loop, Action callback, bool isAfterUseCountDown)
        {
            this.timeStepArgs = timeArgs;
            this.initial_Loop = this.loop = loop;
            this.callback = callback;
            this.taskType = TIMETASKTYPE.INTERVALTASK;
            this.countDown = isAfterUseCountDown ? 0 : this.timeStepArgs;
        }

        public override Task OnTick(int timeArgs)
        {
            base.OnTick(timeArgs);

            if (isPause || countDown > 0)
                return null;
            try
            {
                callback?.Invoke();
                countDown = timeStepArgs;
                if (--loop == 0)
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