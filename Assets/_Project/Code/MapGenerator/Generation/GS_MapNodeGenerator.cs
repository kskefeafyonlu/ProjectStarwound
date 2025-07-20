using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.MapGenerator
{
    [CreateAssetMenu(fileName = "GS_MapNodeInit", menuName = "MapGenerator/GenerationSteps/GS_MapNodeInit")]
    public class GS_MapNodeGenerator : GenerationStep
    {
        public int xBlockCount = 5;
        public int maxYNodeCount = 3;
        public int minYNodeCount = 1;

        public override void ExecuteStep(GameMap map)
        {
            Debug.Log("Executing Map Node Generation Step");
            
            map.mapNodeBlocks = new List<MapNodeBlock>();

            for (int x = 0; x < xBlockCount; x++)
            {
                MapNodeBlock block = new MapNodeBlock();

                // First and last block: only one node
                if (x == 0 || x == xBlockCount - 1)
                {
                    block.NodeCount = 1;
                }
                else
                {
                    block.NodeCount = Random.Range(minYNodeCount, maxYNodeCount + 1);
                }

                
                for (int y = 0; y < block.NodeCount; y++)
                {
                    
                    Vector2 nodeId = new Vector2(x, y);
                    string nodeName = $"Node_{nodeId}";
                    
                    NodeType type;
                    switch (x)
                    {
                        case 0:
                            type = NodeType.Start;
                            break;
                        case var last when last == xBlockCount - 1:
                            type = NodeType.End;
                            break;
                        default:
                            type = NodeType.Normal;
                            break;
                    }

                    block.nodes.Add(new MapNode(nodeName, nodeId, type));
                }

                map.mapNodeBlocks.Add(block);
            }
        }
    }
}