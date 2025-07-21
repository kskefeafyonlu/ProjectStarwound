using System.Collections.Generic;

namespace _Project.Code.MapGenerator
{
    public class MapNodeBlock
    {
        public int NodeCount = 1;
        
        public List<MapNode> nodes;
        
        public MapNodeBlock()
        {
            nodes = new List<MapNode>();
        }
        
        
    }
}