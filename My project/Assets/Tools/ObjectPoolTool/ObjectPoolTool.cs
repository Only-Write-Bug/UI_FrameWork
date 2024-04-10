namespace Tools.ObjectPoolTool
{
    public static class ObjectPoolTool
    {
        public static ObjectPoolModel<T> CreateObjectPool<T>() where T : IRecycle, new()
        {
            return ObjectPoolsManager.Init.CreateObjectPool<T>();
        }
        
        public static ObjectPoolModel<T> GetObjectPoolByName<T>(string objPoolName) where T : IRecycle, new()
        {
            return ObjectPoolsManager.Init.GetObjectPoolByName<T>(objPoolName);
        }
    }
}