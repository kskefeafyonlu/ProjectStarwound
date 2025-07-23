using _Project.Code.MapGenerator;
using UnityEngine;

namespace _Project.Code.MapGenerators
{
    public class MB_SystemMapNode : MonoBehaviour
    {
        public SystemMapNode SystemMapNode;

        public void SetMapNode(SystemMapNode systemMapNode)
        {
            SystemMapNode = systemMapNode;
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