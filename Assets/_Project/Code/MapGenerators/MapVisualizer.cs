using System;
using System.Collections.Generic;
using _Project.Code.SystemMapGenerator;
using UnityEngine;

namespace _Project.Code.MapGenerator
{
    public class MapVisualizer : MonoBehaviour
    {
        public Transform mapParent;
        public GameObject nodePrefab;
        public GameObject connectionPrefab;

        [HideInInspector] public GameMap map;

        public float nodeSpacingX = 100.0f;
        public float nodeSpacingY = 100.0f;
        public float nodeOffsetX = 0.0f;
        public float nodeOffsetY = 0.0f;

        public List<GameObject> nodeObjects = new List<GameObject>();
        private List<GameObject> connectionObjects = new List<GameObject>();
        
        private Dictionary<MapNode, GameObject> nodeToObject = new Dictionary<MapNode, GameObject>();

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

            ClearMapObjects();
            
            VisualizeNodes(gameMap);
            VisualizeConnections(gameMap);
        }

        private void VisualizeConnections(GameMap gameMap)
        {
            Debug.Log("Visualizing Connections for Map with " + gameMap.nodeConnections.Count + " connections.");

            foreach (var connection in gameMap.nodeConnections)
            {
                if (connection.NodeA == null || connection.NodeB == null) continue;
                if (!nodeToObject.ContainsKey(connection.NodeA) || !nodeToObject.ContainsKey(connection.NodeB)) continue;

                Vector3 start = nodeToObject[connection.NodeA].GetComponent<RectTransform>().anchoredPosition;
                Vector3 end = nodeToObject[connection.NodeB].GetComponent<RectTransform>().anchoredPosition;

                GameObject lineObj = Instantiate(connectionPrefab, mapParent);
                RectTransform lineRect = lineObj.GetComponent<RectTransform>();
                if (lineRect != null)
                {
                    lineRect.anchoredPosition = (start + end) / 2f;
                    Vector3 dir = end - start;
                    float length = dir.magnitude;
                    lineRect.sizeDelta = new Vector2(length, lineRect.sizeDelta.y);
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    lineRect.rotation = Quaternion.Euler(0, 0, angle);
                }

                connectionObjects.Add(lineObj); // Add to list for clearing later
            }
        }
        
        


        private void ClearMapObjects()
        {
            foreach (var obj in nodeObjects)
            {
                Destroy(obj);
            }
            nodeObjects.Clear();

            foreach (var obj in connectionObjects)
            {
                Destroy(obj);
            }
            connectionObjects.Clear();
        }

        private void VisualizeNodes(GameMap gameMap)
        {
            nodeToObject.Clear();
            for (int nodeBlockId = 0; nodeBlockId < gameMap.mapNodeBlocks.Count; nodeBlockId++)
            {
                MapNodeBlock nodeBlock = gameMap.mapNodeBlocks[nodeBlockId];
                List<Vector3> nodePositions = CalculatePositionOfNode(nodeBlock.NodeCount, nodeBlockId);

                for (int i = 0; i < nodeBlock.NodeCount; i++)
                {
                    MapNode node = nodeBlock.nodes[i];
                    GameObject nodeObject = InstantiateNode(node, nodePositions[i]);
                    nodeObjects.Add(nodeObject);
                    nodeToObject[node] = nodeObject;
                }
            }
        }

        private void ClearNodeObjects()
        {
            foreach (var obj in nodeObjects)
            {
                Destroy(obj);
            }
            nodeObjects.Clear();
        }

        private GameObject InstantiateNode(MapNode node, Vector3 position)
        {
            GameObject nodeObject = Instantiate(nodePrefab, mapParent);
            nodeObject.name = node.NodeName;

            RectTransform rectTransform = nodeObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(position.x, position.y);
            }

            return nodeObject;
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
                float x = nodeOffsetX + (nodeBlockId * nodeSpacingX);
                float y = nodeOffsetY;
                positions.Add(new Vector3(x, y, 0));
            }
            else
            {
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