using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.MapGenerator
{
    [Serializable]
    public class SystemMapGenerationAlgo
    {
        [HideInInspector] public SystemMap map;


        
        public List<GenerationStep> GenerationSteps = new List<GenerationStep>();

        
        
        public SystemMap GenerateMap()
        {
            map = new SystemMap();
            
            GenerationSteps.Sort((a, b) => a.Order.CompareTo(b.Order));
            
            

            foreach (var step in GenerationSteps)
            {
                step.ExecuteStep(map);
            }

            return map;
        }
        
    }
}