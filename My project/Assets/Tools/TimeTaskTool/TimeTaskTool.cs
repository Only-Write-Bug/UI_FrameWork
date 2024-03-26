using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.TimeTaskTool;
using UnityEngine;

public static class TimeTaskTool
{
    private static TimeTaskManager _timeTaskManager = null;

    public static void Awake()
    {
        _timeTaskManager = TimeTaskManager.Init;
    }

    public static void Dispose()
    {
        _timeTaskManager?.Dispose();
    }
    
    public static async Task<int> Create_IntervalTask(int timeArgs, int loop, Action callback, bool isAfterUseCountDown = true)
    {
        return await _timeTaskManager.Create_IntervalTask(timeArgs, loop, callback, isAfterUseCountDown);
    }
    
    public static async Task<int> Create_ScheduledTask(int timeArgs, Action callback)
    {
        return await _timeTaskManager.Create_ScheduledTask(timeArgs, callback);
    }
}
