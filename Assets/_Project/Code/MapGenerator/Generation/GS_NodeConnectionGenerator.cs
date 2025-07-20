using UnityEngine;

namespace _Project.Code.MapGenerator
{
    
    [CreateAssetMenu(fileName = "GS_NodeConnectionGenerator", menuName = "MapGenerator/GenerationSteps/GS_NodeConnectionGenerator")]
    public class GS_NodeConnectionGenerator : GenerationStep
    {
        public enum ConnectionGenerationType
        {
            Standard
        }

        public ConnectionGenerationType Type = ConnectionGenerationType.Standard;


        public override void ExecuteStep(GameMap map)
        {
            switch (Type)
            {
                case ConnectionGenerationType.Standard:
                    GenerateStandardConnections(map);
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
        
        
    }
}