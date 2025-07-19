using System;
using System.Collections.Generic;

namespace _Project.Code.MapGenerator
{
    [Serializable]
    public class MapGenerationAlgo
    {
        public List<GenerationStep> GenerationSteps = new List<GenerationStep>();
        
        
        
        public void GenerateMap()
        {
            GenerationSteps.Sort((a, b) => a.Order.CompareTo(b.Order));
            

            foreach (var step in GenerationSteps)
            {
                step.ExecuteStep();
            }
        }
        
    }
}