using UnityEngine;

namespace _Project.Code.MapGenerators.StarMapGeneration
{
    
    public enum StarNodeType
    {
        Start,
        Normal,
        End
    }
    public class StarNode
    {
        public string NodeName = "DefaultNode";
        public StarNodeType NodeType = StarNodeType.Normal;
        public Vector2Int ArrayPosition;
        public Vector2 PositionInCell;
        
        
        public StarMap StarMapReference;

        public StarNode(string nodeName, Vector2Int pos,Vector2 posinCell, StarMap starMapReference = null)
        {
            NodeName = nodeName;
            StarMapReference = starMapReference;
            ArrayPosition = pos;
            PositionInCell = posinCell;

        }
    }
}