using System;

namespace Util.Attributes
{
    /// <summary>
    /// 自定义注销特性，切记要传Pulbic的单例方法给SingleMember
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SelfDisposeAttribute : Attribute
    {
        public string SingleMember = null;
    }
}