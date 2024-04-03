using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.TimeTaskTool;
using UnityEngine;

public static class TimeTaskTool
{
    private static readonly TimeTaskManager _timeTaskManager = TimeTaskManager.Init;
    
    public static async Task<int> Create_IntervalTask(int timeArgs, int loop, Action callback, bool isAfterUseCountDown = true)
    {
        return await _timeTaskManager.Create_IntervalTask(timeArgs, loop, callback, isAfterUseCountDown);
    }
    
    public static async Task<int> Create_ScheduledTask(int timeArgs, Action callback)
    {
        return await _timeTaskManager.Create_ScheduledTask(timeArgs, callback);
    }
}
