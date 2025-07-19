using UnityEngine;

namespace _Project.Code.Spaceship
{
    public class Spaceship
    {
        public GameObject[,] ShipComponentGrid;
        public int Width;
        public int Height;
        
        public Spaceship(int width, int height)
        {
            Width = width;
            Height = height;
            ShipComponentGrid = new GameObject[width, height];
        }
        
        public void SetComponent(int x, int y, GameObject component)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                Debug.LogError("Coordinates out of bounds");
                return;
            }
            ShipComponentGrid[x, y] = component;
        }
    }
}
