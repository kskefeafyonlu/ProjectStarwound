using System;
using System.Collections.Generic;
using _Project.Code.MapGenerators.StarMapGeneration;
using _Project.Code.SystemMapGenerator;
using UnityEngine;

namespace _Project.Code.MapGenerator
{
    public class MapVisualizer : MonoBehaviour
    {
        public Transform mapParent;
        public GameObject nodePrefab;
        public GameObject connectionPrefab;

        public Transform starMapParent;
        public GameObject starNodePrefab;

        [HideInInspector] public SystemMap map;
        [HideInInspector] public StarMap starMap;

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
            map = MB_MapManager.Instance.CurrentSystemMap;
            starMap = MB_MapManager.Instance.CurrentStarMap;


            timer += Time.deltaTime;
            if (timer >= updateInterval)
            {
                if (map != null)
                {
                    Debug.Log("Visualizing Map with " + map.mapNodeBlocks.Count + " Node Blocks.");
                    VisualizeMap();
                }

                timer = 0f;
            }
        }


        private Vector2 GetStarNodePosition(StarNode starNode, RectTransform rectTransform)
        {
            RectTransform parentRect = rectTransform.parent as RectTransform;
            if (parentRect == null) return Vector2.zero;

            float parentWidth = parentRect.rect.width;
            float parentHeight = parentRect.rect.height;

            int gridX = 6;
            int gridY = 4;

            float cellWidth = parentWidth / gridX;
            float cellHeight = parentHeight / gridY;

            int x = starNode.ArrayPosition.x;
            int y = starNode.ArrayPosition.y;

            float cellOriginX = x * cellWidth;
            float cellOriginY = y * cellHeight;

            float offsetX = starNode.PositionInCell.x * cellWidth;
            float offsetY = starNode.PositionInCell.y * cellHeight;

            return new Vector2(cellOriginX + offsetX, cellOriginY + offsetY);
        }


        public void VisualizeMap()
        {
            ClearMapObjects();

            VisualizeNodes(map);
            VisualizeConnections(map);
            VisualizeStarMap(starMap);
            VisualizeStarConnections(starMap);
        }

        public void VisualizeStarMap(StarMap starMap)
        {
            Debug.Log("Visualizing Star Map with " + starMap.StarNodes.Length + " Star Nodes.");
            foreach (var starNode in starMap.StarNodes)
            {
                if (starNode == null) continue;
                GameObject starNodeObject = Instantiate(starNodePrefab, starMapParent);


                RectTransform rectTransform = starNodeObject.GetComponent<RectTransform>();
                if (rectTransform != null)
                {
                    rectTransform.anchoredPosition = GetStarNodePosition(starNode, rectTransform);
                }


                nodeObjects.Add(starNodeObject);
            }
        }


        public float starConnectionThickness = 4f;
        public Vector2 starConnectionOffset = Vector2.zero;
        public float starConnectionScaleX = 1f; // Separate X scaling
        public float starConnectionScaleY = 1f; // Separate Y scaling

        public void VisualizeStarConnections(StarMap starMap)
        {
            Debug.Log("Visualizing Star Connections for Map with " + starMap.StarNodes.Length + " Star Nodes.");

            var drawnConnections = new HashSet<string>();

            foreach (var starNode in starMap.StarNodes)
            {
                if (starNode == null) continue;

                foreach (var connection in starNode.StarNodeConnections)
                {
                    if (connection.NodeA == null || connection.NodeB == null) continue;

                    string key = $"{connection.NodeA.NodeName}_{connection.NodeB.NodeName}";
                    string reverseKey = $"{connection.NodeB.NodeName}_{connection.NodeA.NodeName}";
                    if (drawnConnections.Contains(key) || drawnConnections.Contains(reverseKey)) continue;
                    drawnConnections.Add(key);

                    Vector2 startRaw =
                        GetStarNodePosition(connection.NodeA, starMapParent.GetComponent<RectTransform>()) +
                        starConnectionOffset;
                    Vector2 endRaw =
                        GetStarNodePosition(connection.NodeB, starMapParent.GetComponent<RectTransform>()) +
                        starConnectionOffset;

                    Vector2 start = new Vector2(startRaw.x * starConnectionScaleX, startRaw.y * starConnectionScaleY);
                    Vector2 end = new Vector2(endRaw.x * starConnectionScaleX, endRaw.y * starConnectionScaleY);

                    GameObject lineObj = Instantiate(connectionPrefab, starMapParent);
                    RectTransform lineRect = lineObj.GetComponent<RectTransform>();
                    if (lineRect != null)
                    {
                        lineRect.anchoredPosition = (start + end) / 2f -
                                                    new Vector2(lineRect.sizeDelta.x / 2f, lineRect.sizeDelta.y / 2f);
                        Vector2 dir = end - start;
                        float length = dir.magnitude;
                        // Use average scale for thickness
                        float avgScale = (starConnectionScaleX + starConnectionScaleY) / 2f;
                        lineRect.sizeDelta = new Vector2(length, starConnectionThickness * avgScale);
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        lineRect.rotation = Quaternion.Euler(0, 0, angle);
                    }

                    connectionObjects.Add(lineObj);
                }
            }
        }

        private void VisualizeConnections(SystemMap systemMap)
        {
            Debug.Log("Visualizing Connections for Map with " + systemMap.nodeConnections.Count + " connections.");

            foreach (var connection in systemMap.nodeConnections)
            {
                if (connection.NodeA == null || connection.NodeB == null) continue;
                if (!nodeToObject.ContainsKey(connection.NodeA) ||
                    !nodeToObject.ContainsKey(connection.NodeB)) continue;

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

        private void VisualizeNodes(SystemMap systemMap)
        {
            nodeToObject.Clear();
            for (int nodeBlockId = 0; nodeBlockId < systemMap.mapNodeBlocks.Count; nodeBlockId++)
            {
                SystemMapNodeBlock nodeBlock = systemMap.mapNodeBlocks[nodeBlockId];
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