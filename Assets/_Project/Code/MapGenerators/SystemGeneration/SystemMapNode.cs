using _Project.Code.MapGenerators.StarMapGeneration;
using UnityEngine;

namespace _Project.Code.MapGenerator
{
    public enum NodeType
    {
        Start,
        End,
        Normal
    }
    
    public class SystemMapNode
    {
        public string NodeName = "DefaultNode";
        public NodeType Type = NodeType.Normal;

        public StarMap StarMap;
        
        
        public SystemMapNode(string nodeName, NodeType type = NodeType.Normal)
        {
            NodeName = nodeName;

            Type = type;
        }
        
    }
}