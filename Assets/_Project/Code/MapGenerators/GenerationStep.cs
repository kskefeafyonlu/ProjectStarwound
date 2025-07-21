using UnityEngine;

namespace _Project.Code.MapGenerator
{
    
    
    public abstract class GenerationStep : ScriptableObject
    {
        public int Order = 0;
        
        public virtual void ExecuteStep(SystemMap map)
        {
            
        }
    }
    
    
    
}