using System;

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
    }
}