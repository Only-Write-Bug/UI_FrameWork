using System.Collections.Generic;

namespace Util
{
    public class XMLDataNode
    {
        private XMLDataNode _parent = null;
        public virtual XMLDataNode parent
        {
            get => _parent;
            set
            {
                if (value == null)
                    return;
                value.childen.Add(this);
                _parent = value;
            }
        }
        
        public List<XMLDataNode> childen = new List<XMLDataNode>();
    }

    public class XMLDataRoot : XMLDataNode
    {
        public override XMLDataNode parent => null;
    }
}