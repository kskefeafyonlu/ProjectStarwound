using _Project.Code.MapGenerator;
using UnityEngine;

namespace _Project.Code.SystemMapGenerator
{
    public class MB_MapManager : MonoBehaviour
    {
        public static MB_MapManager Instance;

        public GameMap CurrentMap;
        public SystemMapGenerationAlgo SystemGenerationAlgorithm;
        public 


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
                CurrentMap = SystemGenerationAlgorithm.GenerateMap();
            }
            else
            {
                Debug.LogError("No generation algorithm set!");
            }
        }

        public void GenerateStarMap()
        {
            if (SystemGenerationAlgorithm != null)
            {
                
            }
            else
            {
                Debug.LogError("No generation algorithm set!");
            }

        }
    }
}
