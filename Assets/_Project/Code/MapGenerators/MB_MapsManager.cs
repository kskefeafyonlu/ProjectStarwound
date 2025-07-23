using _Project.Code.MapGenerator;
using _Project.Code.MapGenerators.StarMapGeneration;
using UnityEngine;

namespace _Project.Code.SystemMapGenerator
{
    public class MB_MapsManager : MonoBehaviour
    {
        public static MB_MapsManager Instance;

        public SystemMap CurrentSystemMap;
        public SystemMapNode CurrentSystemMapNode;

        public StarMap CurrentStarMap;
        public StarNode CurrentStarNode;

        public string CurrentSystemName = "DefaultSystem";
        public string CurrentStarName = "DefaultStar";

        public SystemMapGenerationAlgo SystemGenerationAlgorithm;
        public StarMapGenerationAlgo StarMapGenerationAlgorithm;

        private void Awake()
        {
            InitializeSingleton();
        }

        private void InitializeSingleton()
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
            if (!IsGenerationAlgorithmSet())
            {
                Debug.LogError("No generation algorithm set!");
                return;
            }

            CurrentSystemMap = SystemGenerationAlgorithm.GenerateMap();
            GenerateStarMapsForSystem(CurrentSystemMap);

            CurrentStarMap = GetFirstStarMapFromSystem(CurrentSystemMap);
        }

        private bool IsGenerationAlgorithmSet()
        {
            return SystemGenerationAlgorithm != null && StarMapGenerationAlgorithm != null;
        }

        private void GenerateStarMapsForSystem(SystemMap systemMap)
        {
            StarMapGenerationAlgorithm.GenerateMap(systemMap);
        }

        private StarMap GetFirstStarMapFromSystem(SystemMap systemMap)
        {
            if (systemMap?.mapNodeBlocks != null &&
                systemMap.mapNodeBlocks.Count > 0 &&
                systemMap.mapNodeBlocks[0].nodes.Count > 0)
            {
                return systemMap.mapNodeBlocks[0].nodes[0].StarMap;
            }
            return null;
        }

        public void SelectSystemNode(SystemMapNode systemMapNode)
        {
            if (!IsValidSystemNode(systemMapNode))
            {
                Debug.LogError("Selected Map Node or its Star Map is null!");
                return;
            }

            CurrentSystemMapNode = systemMapNode;
            CurrentStarMap = systemMapNode.StarMap;
            CurrentStarNode = GetFirstStarNodeFromStarMap(systemMapNode.StarMap);
            CurrentSystemName = systemMapNode.NodeName;
        }

        private bool IsValidSystemNode(SystemMapNode node)
        {
            return node != null && node.StarMap != null;
        }

        private StarNode GetFirstStarNodeFromStarMap(StarMap starMap)
        {
            return starMap?.GetFirstStarNode();
        }

        public void SelectStarNode(StarNode starNode)
        {
            if (!IsValidStarNode(starNode))
            {
                Debug.LogError("Current Star Map or selected Star Node is null!");
                return;
            }

            CurrentStarNode = starNode;
            CurrentStarName = starNode.NodeName;

            // Additional logic for star node selection can be added here
        }

        private bool IsValidStarNode(StarNode node)
        {
            return CurrentStarMap != null && node != null;
        }
    }
}