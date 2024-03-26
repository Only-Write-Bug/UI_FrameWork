using System;
using System.Threading;
using System.Threading.Tasks;
using Tools.TimeTaskTool.Taskscontainer;
using Tools.TimeTaskTool.TimeTask;

namespace Tools.TimeTaskTool
{
    public class TimeTaskManager
    {
        //============================================= const members =============================================
        private const int timeStep = 500;
        //============================================= Init members =============================================
        private static TimeTaskManager _init = null;
        public static TimeTaskManager Init => _init ??= new TimeTaskManager();
        
        private TimeTaskManager()
        {
            isAwake = true;
            _taskscontainer = new TasksContainer();
            
            _timer = new Timer(Tick, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(timeStep));
        }
        //============================================= public variables =============================================

        //============================================= private variables =============================================
        private bool isAwake = false;
        private Timer _timer = null;

        private TasksContainer _taskscontainer = null;
        
        private static SemaphoreSlim _createRequestLock = new SemaphoreSlim(1);

        //============================================= life_circle members =============================================
        public void Dispose()
        {
            _timer.Dispose();
        }
        
        private void Tick(object state)
        {
            _taskscontainer.Traverse_NodesOnTick(timeStep);
        }
        
        //============================================= public functions =============================================
        public async Task<int> Create_IntervalTask(int timeArgs, int loop, Action callback, bool isAfterUseCountDown)
        {
            await _createRequestLock.WaitAsync();

            try
            {
                IntervalTask task = new IntervalTask(timeArgs, loop, callback, isAfterUseCountDown);
                _taskscontainer.Add_Node(task);
                return task.get_TaskID;
            }
            finally
            {
                _createRequestLock.Release();
            }
        }
        
        public async Task<int> Create_ScheduledTask(int timeArgs, Action callback)
        {
            await _createRequestLock.WaitAsync();

            try
            {
                ScheduledTask task = new ScheduledTask(timeArgs, callback);
                _taskscontainer.Add_Node(task);
                await Task.Delay(1);
                return task.get_TaskID;
            }
            finally
            {
                _createRequestLock.Release();
            }
        }

        public void Remove_TaskByTaskID(int taskID)
        {
            _taskscontainer.Remove_NodeByTaskID(taskID);
        }
        //============================================= private functions =============================================
    }
}