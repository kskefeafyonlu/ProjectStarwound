using System.Collections.Generic;

namespace _Project.Code.MapGenerator
{
    public class SystemMapNodeBlock
    {
        public int NodeCount = 1;
        
        public List<MapNode> nodes;
        
        public SystemMapNodeBlock()
        {
            nodes = new List<MapNode>();
        }
        
        
    }
}