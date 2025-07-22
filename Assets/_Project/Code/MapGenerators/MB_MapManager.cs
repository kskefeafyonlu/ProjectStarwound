using _Project.Code.MapGenerator;
using _Project.Code.MapGenerators.StarMapGeneration;
using UnityEngine;

namespace _Project.Code.SystemMapGenerator
{
    public class MB_MapManager : MonoBehaviour
    {
        public static MB_MapManager Instance;

        public SystemMap CurrentSystemMap;
        public StarMap CurrentStarMap;

        public SystemMapGenerationAlgo SystemGenerationAlgorithm;
        public StarMapGenerationAlgo StarMapGenerationAlgorithm;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }




        public void GenerateSystemMap()
        {
            if (SystemGenerationAlgorithm != null)
            {
                CurrentSystemMap = SystemGenerationAlgorithm.GenerateMap();
                StarMapGenerationAlgorithm.GenerateMap(CurrentSystemMap);
                
                CurrentStarMap = CurrentSystemMap.mapNodeBlocks[0].nodes[0].StarMap;
            }
            else
            {
                Debug.LogError("No generation algorithm set!");
            }
        }

        
        
        
        
        public void SelectNewSystemNode(SystemMap systemMap)
        {
            if (systemMap != null)
            {
                CurrentSystemMap = systemMap;
                CurrentStarMap = CurrentSystemMap.mapNodeBlocks[0].nodes[0].StarMap;
            }
            else
            {
                Debug.LogError("Selected System Map is null!");
            }
        }

        public void SelectNewStarNode(StarMap starMap)
        {
            if (starMap != null)
            {
                CurrentStarMap = starMap;
            }
            else
            {
                Debug.LogError("Selected Star Map is null!");
            }


        }
    }
}
