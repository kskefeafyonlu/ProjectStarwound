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
        public NodeType Type = NodeType.Normal;

        
        
        
        public MapNode(string nodeName, NodeType type = NodeType.Normal)
        {
            NodeName = nodeName;

            Type = type;
        }
        
    }
}