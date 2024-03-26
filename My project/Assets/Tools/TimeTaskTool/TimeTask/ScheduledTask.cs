using System;

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
    }
}