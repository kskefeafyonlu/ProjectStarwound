using _Project.Code.MapGenerator;
using UnityEngine;

public class MB_MapManager : MonoBehaviour
{
    public MB_MapManager Instance;
    
    public GameMap CurrentMap;
    public MapGenerationAlgo GenerationAlgorithm;
    
    
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
    
    
    public void GenerateMap()
    {
        if (GenerationAlgorithm != null)
        {
            CurrentMap = GenerationAlgorithm.GenerateMap();
        }
        else
        {
            Debug.LogError("No generation algorithm set!");
        }
    }
    

}
