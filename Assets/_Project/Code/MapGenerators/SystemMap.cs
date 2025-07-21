using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.MapGenerator
{
    
    [System.Serializable]
    public class SystemMap
    {
        
        public List<SystemMapNodeBlock> mapNodeBlocks;
        
        public List<NodeConnection> nodeConnections;

        public SystemMap()
        {
            mapNodeBlocks = new List<SystemMapNodeBlock>();
            nodeConnections = new List<NodeConnection>();

        }
        
    }
}