using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.MapGenerator
{
    
    [System.Serializable]
    public class GameMap
    {
        
        public List<MapNodeBlock> mapNodeBlocks;
        
        public List<NodeConnection> nodeConnections;

        public GameMap()
        {
            mapNodeBlocks = new List<MapNodeBlock>();
            nodeConnections = new List<NodeConnection>();

        }
        
    }
}