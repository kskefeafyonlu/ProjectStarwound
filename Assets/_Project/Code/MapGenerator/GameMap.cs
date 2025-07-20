using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.MapGenerator
{
    
    [System.Serializable]
    public class GameMap
    {
        public int width = 5;
        
        public List<MapNodeBlock> mapNodeBlocks;

        public GameMap()
        {
            mapNodeBlocks = new List<MapNodeBlock>();

        }
        
    }
}