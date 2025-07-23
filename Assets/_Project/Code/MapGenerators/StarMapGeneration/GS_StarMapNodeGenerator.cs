using _Project.Code.MapGenerator;
using UnityEngine;

namespace _Project.Code.MapGenerators.StarMapGeneration
{
    
    [CreateAssetMenu(fileName = "GS_StarMapNodeGenerator", menuName = "MapGenerator/StarMapNodeGenerator")]
    public class GS_StarMapNodeGenerator : GenerationStep
    {
        public StarMap CurrentStarMap;


        public GS_StarMapNodeGenerator()
        {
            CurrentStarMap = new StarMap();
            Order = 3;
        }


       
        public override void ExecuteStep(SystemMap map)
        {
            if (map == null || CurrentStarMap == null)
            {
                Debug.LogError("SystemMap or StarMap is null in GS_StarMapNodeGenerator.");
                return;
            }

            foreach (var nodeBlock in map.mapNodeBlocks)
            {
                foreach (var node in nodeBlock.nodes)
                {
                    var starMap = new StarMap();

                    for (var x = 0; x < 6; x++) // 6 columns
                    {
                        for (var y = 0; y < 4; y++) // 4 rows
                        {
                            starMap.StarNodes[x, y] = new StarNode(
                                $"{node.NodeName}_{x}_{y}",
                                new Vector2Int(x, y),
                                new Vector2(
                                    Random.Range(0f, 1f),
                                    Random.Range(0f, 1f)
                                ),
                                starMap
                            );
                        }
                    }

                    node.StarMap = starMap; // <-- Fix: assign the generated StarMap to the node
                }
            }
            
            // Select a random start point in the first block of the map
            SelectMapStartPoint(map);
        }
        
        
        public void SelectMapStartPoint(SystemMap map)
        {
            if (map == null || map.mapNodeBlocks == null || map.mapNodeBlocks.Count == 0)
            {
                Debug.LogError("SystemMap or its node blocks are null/empty.");
                return;
            }

            var firstBlock = map.mapNodeBlocks[0];
            if (firstBlock.nodes == null || firstBlock.nodes.Count == 0)
            {
                Debug.LogError("First block has no nodes.");
                return;
            }

            //random index from 0 to NodeCount - 1
            int randomIndex = Random.Range(0, firstBlock.NodeCount);
            var startNode = firstBlock.nodes[randomIndex];
            startNode.Type = NodeType.Start; // Set the type of the selected node to Start
            
        }
    }
}