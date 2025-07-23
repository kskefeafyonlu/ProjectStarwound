using System.Collections.Generic;

namespace _Project.Code.MapGenerators.StarMapGeneration
{
    public class StarMap
    {
        public StarNode[,] StarNodes = new StarNode[6, 4];


        public StarNode GetFirstStarNode()
        {
            if (StarNodes == null || StarNodes.GetLength(0) == 0 || StarNodes.GetLength(1) == 0)
                return null;

            for (int y = 0; y < StarNodes.GetLength(1); y++)
            {
                if (StarNodes[0, y] != null)
                    return StarNodes[0, y];
            }
            return null;
        }
    }
}