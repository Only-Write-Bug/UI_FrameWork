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