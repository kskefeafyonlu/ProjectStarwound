namespace _Project.Code.MapGenerator
{
    
    
    public abstract class GenerationStep
    {
        public int Order = 0;
        
        public virtual void ExecuteStep()
        {
            
        }
    }
    
    
    public class GS_MapBackgroundGeneration : GenerationStep
    {
        public override void ExecuteStep()
        {
            // Logic for generating the map background
            // This could involve setting up terrain, skybox, etc.
            UnityEngine.Debug.Log("Generating Map Background");
        }
    }
    
    
}