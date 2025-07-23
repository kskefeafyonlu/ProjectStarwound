using _Project.Code.MapGenerator;
using UnityEngine;

namespace _Project.Code.MapGenerators
{
    public class MB_MapNode : MonoBehaviour
    {
        public MapNode MapNode;

        public void SetMapNode(MapNode mapNode)
        {
            MapNode = mapNode;
        }
        
        // public void Select()
        // {
        //     if (MapNode != null)
        //     {
        //         MB_MapManager.Instance.SelectSystemNode(MapNode);
        //     }
        // }
        //
        // public void Deselect()
        // {
        //     MB_MapManager.Instance.DeselectSystemNode();
        // }
    }
}