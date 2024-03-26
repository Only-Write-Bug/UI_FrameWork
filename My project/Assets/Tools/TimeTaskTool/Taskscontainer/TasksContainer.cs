using System.Collections.Generic;
using Tools.TimeTaskTool.TimeTask;

namespace Tools.TimeTaskTool.Taskscontainer
{
    public class TasksContainer
    {
        //============================================= public variables =============================================
        public static int taskCount = 0;

        //============================================= private variables =============================================
        private TaskNode _startNode = null;
        private TaskNode _endNode = null;
        private TaskNode _curNode = null;

        //17是一个质数，由于Task数量一般较多，所以给一个较大的质数初始值，减少扩容次数和哈希冲突
        private Dictionary<int, TaskNode> nodeContainerByTaskID = new Dictionary<int, TaskNode>(17);

        //============================================= private variables =============================================
        public void Add_Node(TimeTaskBase task)
        {
            var tmpNode = new TaskNode(task);

            if (_startNode == null)
            {
                _startNode = _endNode = _curNode = tmpNode;
                tmpNode.rightNeighbarNode = tmpNode;
            }
            else
            {
                var nextNode = _curNode.rightNeighbarNode;
                _curNode.rightNeighbarNode = tmpNode;
                tmpNode.rightNeighbarNode = nextNode;
                _curNode = tmpNode;
            }

            taskCount++;
            nodeContainerByTaskID.Add(task.get_TaskID, tmpNode);
        }

        public TimeTaskBase Get_TaskByTaskID(int taskID)
        {
            return nodeContainerByTaskID.TryGetValue(taskID, out var node) ? node.task : null;
        }

        public void Remove_NodeByTaskID(int taskID)
        {
            Get_TaskByTaskID(taskID).OnDispose();
            taskCount--;
            if (taskCount <= 0)
                _startNode = _endNode = _curNode = null;
            else
                _curNode.leftNeighbarNode.rightNeighbarNode = _curNode.rightNeighbarNode;
            nodeContainerByTaskID.Remove(taskID);
        }

        public void Traverse_NodesOnTick(int timeArgs)
        {
            if (taskCount <= 0)
                return;
            if (taskCount == 1)
            {
                _curNode.task.OnTick(timeArgs);
                return;
            }

            var rightPtr = _startNode;
            var leftPtr = _startNode.leftNeighbarNode;

            while (true)
            {
                if (rightPtr == leftPtr)
                    break;
                rightPtr.task.OnTick(timeArgs);
                rightPtr = rightPtr.rightNeighbarNode;

                if (leftPtr == rightPtr)
                    break;
                leftPtr.task.OnTick(timeArgs);
                leftPtr = leftPtr.leftNeighbarNode;
            }
        }
    }
}