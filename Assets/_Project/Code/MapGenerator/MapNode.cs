using UnityEngine;

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
        public Vector2 NodeId = new Vector2();
        public NodeType Type = NodeType.Normal;
        
        
        
        public MapNode(string nodeName, Vector2 nodeId, NodeType type = NodeType.Normal)
        {
            NodeName = nodeName;
            NodeId = nodeId;
            Type = type;
        }
        
    }
}