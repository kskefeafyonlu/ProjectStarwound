using UnityEngine;

namespace _Project.Code.MapGenerators.StarMapGeneration
{
    public class MB_StarMapNode : MonoBehaviour
    {
        public StarNode StarNode;
        
        public void SetStarNode(StarNode starNode)
        {
            StarNode = starNode;
        }
    }
}