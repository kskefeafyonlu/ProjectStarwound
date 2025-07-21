using System;
using System.Collections.Generic;
using _Project.Code.MapGenerator;
using UnityEngine;

namespace _Project.Code.MapGenerators.StarMapGeneration
{
    [Serializable]
    public class StarMapGenerationAlgo
    {
        [HideInInspector] public SystemMap map;



        public List<GenerationStep> GenerationSteps = new List<GenerationStep>();



        public SystemMap GenerateMap(SystemMap smap)
        {
            map = smap;

            GenerationSteps.Sort((a, b) => a.Order.CompareTo(b.Order));



            foreach (var step in GenerationSteps)
            {
                step.ExecuteStep(map);
            }

            return map;
        }
    }
}