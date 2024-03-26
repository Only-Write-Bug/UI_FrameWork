using Tools.TimeTaskTool.TimeTask;

namespace Tools.TimeTaskTool.Taskscontainer
{
    public class TaskNode
    {
        public TaskNode(TimeTaskBase curTask)
        {
            task = curTask;
        }
        
        internal bool isPause = false;
        public TimeTaskBase task = null;
        
        private TaskNode _leftNeighbarNode = null;
        public TaskNode leftNeighbarNode
        {
            get => _leftNeighbarNode;
            internal set
            {
                _leftNeighbarNode = value;
                if (value != null && value.rightNeighbarNode != this)
                    value.rightNeighbarNode = this;
            }
        }
        
        private TaskNode _rightNeighbarNode = null;
        public TaskNode rightNeighbarNode
        {
            get => _rightNeighbarNode;
            internal set
            {
                _rightNeighbarNode = value;
                if (value != null && value.leftNeighbarNode != this)
                    value.leftNeighbarNode = this;
            }
        }

        internal void RemoveThis()
        {
            rightNeighbarNode.leftNeighbarNode = leftNeighbarNode;
        }
    }
}