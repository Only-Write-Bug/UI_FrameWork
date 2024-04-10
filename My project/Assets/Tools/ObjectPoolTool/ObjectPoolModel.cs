using System.Collections.Generic;
using Unity.VisualScripting;

namespace Tools.ObjectPoolTool
{
    public class ObjectPoolModel<T> where T :IRecycle, new()
    {
        private int _poolSize = 8;
        public int poolSize
        {
            get => _poolSize;
            set
            {
                _poolSize = value;
                ResetPoolSize();
            }
        }
        
        private string _poolName = null;
        public string get_poolName => _poolName;
        
        private Queue<T> _pool = null;
        private List<T> _inUseObj = new List<T>(8);
        
        public ObjectPoolModel(string customizeName = null, int customizeInitSize = 8)
        {
            _poolName = "objPool_" + (customizeName ?? typeof(T).ToString());
            _poolSize = customizeInitSize;
        }

        public void Init()
        {
            poolSize = _poolSize;
        }
        
        /// <summary>
        /// 回收所有对象
        /// </summary>
        public void RecycleAllObj()
        {
            foreach (var t in _inUseObj)
            {
                if (_pool.Count >= _poolSize)
                    break;
                if(t == null)
                    continue;
                
                RecycleObj(t);
            }

            _inUseObj.Clear();
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="obj"></param>
        public void RecycleObj(T obj)
        {
            if(obj == null)
                return;
            
            obj.Recycle();
            _pool.Enqueue(obj);
            _inUseObj.Remove(obj);
        }
        
        /// <summary>
        /// 重新设置对象池大小
        /// </summary>
        private void ResetPoolSize()
        {
            var tmpPool = new Queue<T>();
            while (tmpPool.Count <= _poolSize)
            {
                if (_pool.Count <= 0)
                    break;
                tmpPool.Enqueue(tmpPool.Dequeue());
            }

            _pool = tmpPool;

            FillUpTheObjectPool();
        }

        /// <summary>
        /// 填充满池子
        /// </summary>
        private void FillUpTheObjectPool()
        {
            while(_pool.Count <= _poolSize)
                _pool.Enqueue(new T());
        }
    }
}