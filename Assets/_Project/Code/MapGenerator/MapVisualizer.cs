using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.MapGenerator
{
    public class MapVisualizer : MonoBehaviour
    {
        public Transform mapParent;
        public GameObject nodePrefab;
        [HideInInspector] public GameMap map;
        
        public float nodeSpacingX = 100.0f;
        public float nodeSpacingY = 100.0f;
        public float nodeOffsetX = 0.0f;
        public float nodeOffsetY = 0.0f;
        
        public List<GameObject> nodeObjects = new List<GameObject>();


        private float timer = 0f;
        private float updateInterval = 0.5f; // 2 times per second

        private void Update()
        {
            map = MB_MapManager.Instance.CurrentMap;
            
            
            timer += Time.deltaTime;
            if (timer >= updateInterval)
            {
                if (map != null)
                {
                    Debug.Log("Visualizing Map with " + map.mapNodeBlocks.Count + " Node Blocks.");
                    VisualizeMap(map);
                }
                timer = 0f;
            }
        }
        
        

        public void VisualizeMap(GameMap gameMap)
        {
            
            foreach (var obj in nodeObjects)
            {
                Destroy(obj);
            }
            nodeObjects.Clear();
            
            

            for (int nodeBlockId = 0; nodeBlockId < gameMap.mapNodeBlocks.Count; nodeBlockId++)
            {
                MapNodeBlock nodeBlock = gameMap.mapNodeBlocks[nodeBlockId];
                List<Vector3> nodePositions = CalculatePositionOfNode(nodeBlock.NodeCount, nodeBlockId);

                for (int i = 0; i < nodeBlock.NodeCount; i++)
                {
                    MapNode node = nodeBlock.nodes[i];
                    GameObject nodeObject = Instantiate(nodePrefab, mapParent);
                    nodeObject.name = node.NodeName;

                    RectTransform rectTransform = nodeObject.GetComponent<RectTransform>();
                    if (rectTransform != null)
                    {
                        rectTransform.anchoredPosition = new Vector2(nodePositions[i].x, nodePositions[i].y);
                    }

                    nodeObjects.Add(nodeObject);
                }
            }
        }



        public List<Vector3> CalculatePositionOfNode(int nodeCount, int nodeBlockId)
        {
            List<Vector3> positions = new List<Vector3>();
            if (nodeCount <= 0)
            {
                return positions;
            }
            else if (nodeCount == 1)
            {
                // Single node, place it at the center of the block
                float x = nodeOffsetX + (nodeBlockId * nodeSpacingX);
                float y = nodeOffsetY;
                positions.Add(new Vector3(x, y, 0));
            }
            else
            {
                // Multiple nodes, distribute them evenly
                float totalHeight = (nodeCount - 1) * nodeSpacingY;
                for (int i = 0; i < nodeCount; i++)
                {
                    float x = nodeOffsetX + (nodeBlockId * nodeSpacingX);
                    float y = nodeOffsetY - (i * nodeSpacingY) + (totalHeight / 2);
                    positions.Add(new Vector3(x, y, 0));
                }
            }
            

            return positions;
        }
    }
}