using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.MapGenerator
{
    public class MapVisualizer : MonoBehaviour
    {
        public Transform mapParent;
        public GameObject nodePrefab;
        [HideInInspector] public GameMap map;
        
        public List<GameObject> nodeObjects = new List<GameObject>();

        public void VisualizeMap(GameMap gameMap)
        {
            foreach (MapNodeBlock nodeBlock in gameMap.mapNodeBlocks)
            {


                List<Vector3> nodePositions = CalculatePositionOfNode(nodeBlock.nodeCount);
                
                
                for (int i = 0; i < nodeBlock.nodeCount; i++)
                {
                    MapNode node = nodeBlock.nodes[i];
                    GameObject nodeObject = Instantiate(nodePrefab, transform);
                    nodeObject.name = node.NodeName;
                    nodeObject.transform.position = nodePositions[i];
                }
                
                
            }
            
        }



        public List<Vector3> CalculatePositionOfNode(int nodeCount)
        {
            List<Vector3> positions = new List<Vector3>();

            


            return positions;
        }
    }
}