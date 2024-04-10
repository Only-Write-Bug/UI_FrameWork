using System;
using System.Collections.Generic;

namespace Tools.ObjectPoolTool
{
    public class ObjectPoolsManager
    {
        private static ObjectPoolsManager _init = null;
        public static ObjectPoolsManager Init => _init ??= new ObjectPoolsManager();

        private Dictionary<string, dynamic> _objPoolDic = new Dictionary<string, dynamic>(7);

        /// <summary>
        /// 创建一个指定类型的对象池
        /// </summary>
        /// <param name="customizeName">可为空，如果为null，则会使用类型为后缀，全称为objPool_*</param>
        /// <param name="customizeInitSize">初始大小，默认为8，可以扩容</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ObjectPoolModel<T> CreateObjectPool<T>(string customizeName = null, int customizeInitSize = 8)
            where T : IRecycle, new()
        {
            var pool = new ObjectPoolModel<T>(customizeName, customizeInitSize);
            if (_objPoolDic.TryGetValue(pool.get_poolName, out var oldPool))
            {
                return oldPool as ObjectPoolModel<T>;
            }

            _objPoolDic.Add(pool.get_poolName, pool);
            pool.Init();
            return pool;
        }

        /// <summary>
        /// 通过对象池名称获取对象池
        /// </summary>
        /// <param name="poolName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ObjectPoolModel<T> GetObjectPoolByName<T>(string poolName) where T : IRecycle, new()
        {
            _objPoolDic.TryGetValue(poolName, out var objPool);
            return (objPool as ObjectPoolModel<T>);
        }
    }
}