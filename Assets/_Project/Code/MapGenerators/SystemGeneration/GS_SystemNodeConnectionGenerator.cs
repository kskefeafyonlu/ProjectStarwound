using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Code.MapGenerator
{
    
    [CreateAssetMenu(fileName = "GS_NodeConnectionGenerator", menuName = "MapGenerator/GenerationSteps/GS_NodeConnectionGenerator")]
    public class GS_SystemNodeConnectionGenerator : GenerationStep
    {
        public enum ConnectionGenerationType
        {
            Standard
        }

        public ConnectionGenerationType Type = ConnectionGenerationType.Standard;
        public float percentageToRemove = 0.2f; // 20% of connections will be removed


        public override void ExecuteStep(GameMap map)
        {
            switch (Type)
            {
                case ConnectionGenerationType.Standard:
                    GenerateStandardConnections(map);
                    RemoveSomeConnections(map, percentageToRemove);
                    break;
                default:
                    throw new System.NotImplementedException($"Connection generation type {Type} is not implemented.");
            }
        }

        private void GenerateStandardConnections(GameMap map)
        {
            map.nodeConnections.Clear();

            for (int blockIdx = 0; blockIdx < map.mapNodeBlocks.Count - 1; blockIdx++)
            {
                var currentBlock = map.mapNodeBlocks[blockIdx];
                var nextBlock = map.mapNodeBlocks[blockIdx + 1];

                foreach (var node in currentBlock.nodes)
                {
                    foreach (var rightNode in nextBlock.nodes)
                    {
                        var connection = new NodeConnection($"{node.NodeName}-{rightNode.NodeName}", node, rightNode);
                        map.nodeConnections.Add(connection);
                    }
                }
                
            }
            
        }

        

private void RemoveSomeConnections(GameMap map, float percentageToRemove)
{
    int removeCount = Mathf.FloorToInt(map.nodeConnections.Count * percentageToRemove);
    var rng = new System.Random();

    // Remove random connections
    var shuffled = map.nodeConnections.OrderBy(_ => rng.Next()).ToList();
    for (int i = 0; i < removeCount; i++)
    {
        map.nodeConnections.Remove(shuffled[i]);
    }

    // Ensure every node has at least one incoming and one outgoing connection
    foreach (var block in map.mapNodeBlocks)
    {
        foreach (var node in block.nodes)
        {
            bool hasOutgoing = map.nodeConnections.Any(c => c.NodeA == node);
            bool hasIncoming = map.nodeConnections.Any(c => c.NodeB == node);

            // If missing outgoing, try to connect to a node in the next block
            if (!hasOutgoing)
            {
                int blockIdx = map.mapNodeBlocks.IndexOf(block);
                if (blockIdx < map.mapNodeBlocks.Count - 1)
                {
                    var nextBlock = map.mapNodeBlocks[blockIdx + 1];
                    var target = nextBlock.nodes.FirstOrDefault();
                    if (target != null)
                    {
                        map.nodeConnections.Add(new NodeConnection($"{node.NodeName}-{target.NodeName}", node, target));
                    }
                }
            }

            // If missing incoming, try to connect from a node in the previous block
            if (!hasIncoming)
            {
                int blockIdx = map.mapNodeBlocks.IndexOf(block);
                if (blockIdx > 0)
                {
                    var prevBlock = map.mapNodeBlocks[blockIdx - 1];
                    var source = prevBlock.nodes.FirstOrDefault();
                    if (source != null)
                    {
                        map.nodeConnections.Add(new NodeConnection($"{source.NodeName}-{node.NodeName}", source, node));
                    }
                }
            }
        }
    }
}
    }
}