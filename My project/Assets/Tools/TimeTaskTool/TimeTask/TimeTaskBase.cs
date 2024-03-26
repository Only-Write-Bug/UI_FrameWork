using System;
using UnityEngine;

namespace Tools.TimeTaskTool.TimeTask
{
    public class TimeTaskBase
    {
        private static int _taskID = -1;
        public int get_TaskID => _taskID;

        public TimeTaskBase() => _taskID++;

        protected int timeStepArgs = 0;
        protected int countDown = 0;

        protected TIMETASKTYPE taskType = TIMETASKTYPE.DEFAULT;

        public bool isPause = false;

        public Action callback = null;
        protected int initial_Loop = 0;
        public int loop = 0;

        public virtual void OnTick(int timeArgs)
        {
            if (isPause)
                return;

            countDown -= timeArgs;
            if (countDown <= 0)
            {
                callback?.Invoke();
                countDown = timeStepArgs;
                switch (taskType)
                {
                    case TIMETASKTYPE.SCHEDULEDTASK:
                        TimeTaskManager.Init.Remove_TaskByTaskID(get_TaskID);
                        break;
                    case TIMETASKTYPE.INTERVALTASK:
                        loop--;
                        if(loop == 0)
                            TimeTaskManager.Init.Remove_TaskByTaskID(get_TaskID);
                        break;
                }
            }
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