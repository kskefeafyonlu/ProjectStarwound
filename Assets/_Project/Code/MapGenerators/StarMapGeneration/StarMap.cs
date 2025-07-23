using System.Collections.Generic;

namespace _Project.Code.MapGenerators.StarMapGeneration
{
    public class StarMap
    {
        public StarNode[,] StarNodes = new StarNode[6, 4];


        public StarNode GetFirstStarNode()
        {
            if (StarNodes != null && StarNodes.Length > 0)
            {
                return StarNodes[0, 0];
            }
            else
            {
                return null;
            }
            
        }
    }
}