using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.MapGenerator
{
    [CreateAssetMenu(fileName = "GS_MapNodeInit", menuName = "MapGenerator/GenerationSteps/GS_MapNodeInit")]
    public class GS_SystemMapNodeGenerator : GenerationStep
    {
        public int xBlockCount = 5;
        public int maxYNodeCount = 3;
        public int minYNodeCount = 1;

        public override void ExecuteStep(SystemMap map)
        {
            
            map.mapNodeBlocks = new List<SystemMapNodeBlock>();

            for (int x = 0; x < xBlockCount; x++)
            {
                SystemMapNodeBlock block = new SystemMapNodeBlock();

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
                    string nodeName = $"Node_{x}_{y}";
                    
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

                    block.nodes.Add(new MapNode(nodeName, type));
                }

                map.mapNodeBlocks.Add(block);
            }
        }
    }
}