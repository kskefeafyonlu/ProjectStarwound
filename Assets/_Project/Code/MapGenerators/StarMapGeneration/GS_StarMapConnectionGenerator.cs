// File: Assets/_Project/Code/MapGenerators/StarMapGeneration/GS_StarMapConnectionGenerator.cs
using _Project.Code.MapGenerator;
using UnityEngine;

namespace _Project.Code.MapGenerators.StarMapGeneration
{
    
    [CreateAssetMenu(fileName = "GS_StarMapConnectionGenerator", menuName = "MapGenerator/StarMapConnectionGenerator")]
    public class GS_StarMapConnectionGenerator : GenerationStep
    {
        public override void ExecuteStep(SystemMap map)
        {
            Debug.Log("Executing GS_StarMapConnectionGenerator...");
            if (map == null) return;

            foreach (var nodeBlock in map.mapNodeBlocks)
            {
                foreach (var node in nodeBlock.nodes)
                {
                    var starMap = node.StarMap;
                    if (starMap == null || starMap.StarNodes == null) continue;

                    int width = starMap.StarNodes.GetLength(0);
                    int height = starMap.StarNodes.GetLength(1);

                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            var starNode = starMap.StarNodes[x, y];
                            if (starNode == null) continue;
                            starNode.StarNodeConnections.Clear();

                            // Connect to left neighbor
                            if (x > 0 && starMap.StarNodes[x - 1, y] != null)
                                starNode.StarNodeConnections.Add(new StarNodeConnection(starNode, starMap.StarNodes[x - 1, y]));
                            // Connect to right neighbor
                            if (x < width - 1 && starMap.StarNodes[x + 1, y] != null)
                                starNode.StarNodeConnections.Add(new StarNodeConnection(starNode, starMap.StarNodes[x + 1, y]));
                            // Connect to top neighbor
                            if (y > 0 && starMap.StarNodes[x, y - 1] != null)
                                starNode.StarNodeConnections.Add(new StarNodeConnection(starNode, starMap.StarNodes[x, y - 1]));
                            // Connect to bottom neighbor
                            if (y < height - 1 && starMap.StarNodes[x, y + 1] != null)
                                starNode.StarNodeConnections.Add(new StarNodeConnection(starNode, starMap.StarNodes[x, y + 1]));
                        }
                    }
                }
            }
        }
    }
}