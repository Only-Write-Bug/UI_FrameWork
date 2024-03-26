using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Init : MonoBehaviour
{
    private void Awake()
    {
        InitAllTools();
        // TimeTaskTool.Create_IntervalTask(5000, 5, () => { Debug.Log("interval task is running");});  
        for (int i = 0; i < 10; i++)
        {
            int value = i;
            TimeTaskTool.Create_ScheduledTask(Random.Range(1,10) * 100, () => { Debug.Log($"scheduled task {value} :: is running"); });
        }
    }

    private void OnDestroy()
    {
        DisposeAllTools();
    }

    private void InitAllTools()
    {
        TimeTaskTool.Awake();
    }

    private void DisposeAllTools()
    {
        TimeTaskTool.Dispose();
    }
}