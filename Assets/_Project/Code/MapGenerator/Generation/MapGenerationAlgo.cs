using System;
using System.Collections.Generic;

namespace _Project.Code.MapGenerator
{
    [Serializable]
    public class MapGenerationAlgo
    {
        public GameMap map;
        
        public List<GenerationStep> GenerationSteps = new List<GenerationStep>();

        
        
        public GameMap GenerateMap()
        {
            map = new GameMap();
            
            GenerationSteps.Sort((a, b) => a.Order.CompareTo(b.Order));
            
            

            foreach (var step in GenerationSteps)
            {
                step.ExecuteStep(map);
            }

            return map;
        }
        
    }
}