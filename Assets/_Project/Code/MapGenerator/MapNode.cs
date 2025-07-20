namespace _Project.Code.MapGenerator
{
    public enum NodeType
    {
        Start,
        End,
        Normal
    }
    
    public class MapNode
    {
        public string NodeName = "DefaultNode"; 
        public int NodeId = 0;
        public NodeType Type = NodeType.Normal;
        
        
        
        public MapNode(string nodeName, int nodeId, NodeType type = NodeType.Normal)
        {
            NodeName = nodeName;
            NodeId = nodeId;
            Type = type;
        }
        
    }
}