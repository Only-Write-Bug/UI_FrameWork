using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tools.EventSystemTool;
using UnityEngine;
using Util.Attributes;
using Random = UnityEngine.Random;

public class Init : MonoBehaviour
{
    private void Awake()
    {
        TimeTaskTool.Create_ScheduledTask(1000, () => { });
    }

    private void OnDestroy()
    {
        TouchOffAllSelfDisposeAttribute();
    }

    /// <summary>
    /// 触发所有添加SelfDispose特性的方法，会根据传入的singleMember决定是否为单例
    /// </summary>
    private void TouchOffAllSelfDisposeAttribute()
    {
        var methods = Assembly.GetExecutingAssembly().GetTypes()
            .SelectMany(t => t.GetMethods())
            .Where(m => m.GetCustomAttribute<SelfDisposeAttribute>() != null);

        string curSingleMember = null;
        foreach (var method in methods)
        {
            curSingleMember = method.GetCustomAttribute<SelfDisposeAttribute>().SingleMember;

            if (curSingleMember != null)
            {
                var declaringType = method.DeclaringType;
                var instanceProperty =
                    declaringType.GetProperty(curSingleMember, BindingFlags.Public | BindingFlags.Static);

                if (instanceProperty == null)
                {
                    Debug.LogWarning($"{declaringType} don't has the single member");
                }
                
                var instance = instanceProperty.GetValue(null);
                method.Invoke(instance, null);
            }
            else
            {
                var instance = Activator.CreateInstance(method.DeclaringType);
                method.Invoke(instance, null);
            }
        }
    }
}