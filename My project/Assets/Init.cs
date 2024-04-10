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
        
    }

    private void OnDestroy()
    {
        TouchOffAllSelfDisposeAttribute();
    }

    /// <summary>
    /// 触发所有添加SelfDispose特性的方法，只有传入了单例成员才会有效销毁指定对象
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
                    break;
                }
                
                var instance = instanceProperty.GetValue(null);
                method.Invoke(instance, null);
            }
        }
    }
}