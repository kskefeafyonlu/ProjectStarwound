using _Project.Code.MapGenerator;
using _Project.Code.MapGenerators.StarMapGeneration;
using UnityEngine;

namespace _Project.Code.SystemMapGenerator
{
    public class MB_MapManager : MonoBehaviour
    {
        public static MB_MapManager Instance;

        public SystemMap CurrentSystemMap;
        public MapNode CurrentMapNode;
        
        public StarMap CurrentStarMap;
        public StarNode CurrentStarNode;
        
        public string CurrentSystemName = "DefaultSystem";
        public string CurrentStarName = "DefaultStar";

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

        

        
        
        
        
        public void SelectSystemNode(MapNode mapNode)
        {
            if (mapNode != null && mapNode.StarMap != null)
            {
                CurrentStarMap = mapNode.StarMap;
                CurrentMapNode = mapNode;
                CurrentStarNode = mapNode.StarMap.GetFirstStarNode();

                
                CurrentSystemName = mapNode.NodeName;
            }
            else
            {
                Debug.LogError("Selected Map Node or its Star Map is null!");
            }
            
        }

        public void SelectStarNode(StarNode starNode)
        {
            if (CurrentStarMap != null && starNode != null)
            {
                CurrentStarNode = starNode;

                CurrentStarName = starNode.NodeName;
                
                // Here you can add additional logic to handle the selected star node
                // For example, you might want to update the UI or load specific data related to the star node.
            }
            else
            {
                Debug.LogError("Current Star Map or selected Star Node is null!");
            }

        }
    }
}
