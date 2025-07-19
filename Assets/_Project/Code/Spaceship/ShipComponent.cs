namespace _Project.Code.Spaceship
{
    public enum ShipComponentType
    {
        Engine,
        Weapon,
        Shield,
        Hull,
        Utility
    }
    
    
    public class ShipComponent
    {
        public string ComponentName = "DefaultComponent";
        public ShipComponentType ComponentType = ShipComponentType.Hull;
        
        public int Width = 1;
        public int Height = 1;
        
    }
}