using System;
using System.Collections.Generic;
using _Project.Code.MapGenerators;
using _Project.Code.MapGenerators.StarMapGeneration;
using _Project.Code.SystemMapGenerator;
using UnityEngine;

namespace _Project.Code.MapGenerator
{
    public class MB_MapsVisualizer : MonoBehaviour
    {
        [Header("System Map")]
        public Transform mapParent;
        public GameObject nodePrefab;
        public GameObject connectionPrefab;

        [Header("Star Map")]
        public Transform starMapParent;
        public GameObject starNodePrefab;

        [Header("Node Layout")]
        public float nodeSpacingX = 100.0f;
        public float nodeSpacingY = 100.0f;
        public float nodeOffsetX = 0.0f;
        public float nodeOffsetY = 0.0f;

        [Header("Star Connection")]
        public float starConnectionThickness = 4f;
        public Vector2 starConnectionOffset = Vector2.zero;
        public float starConnectionScaleX = 1f;
        public float starConnectionScaleY = 1f;

        [HideInInspector] public SystemMap map;
        [HideInInspector] public SystemMapNode SystemMapNode;
        [HideInInspector] public StarMap starMap;
        [HideInInspector] public StarNode starNode;

        private List<GameObject> nodeObjects = new List<GameObject>();
        private List<GameObject> connectionObjects = new List<GameObject>();
        private Dictionary<SystemMapNode, GameObject> nodeToObject = new Dictionary<SystemMapNode, GameObject>();

        private float timer = 0f;
        private float updateInterval = 0.5f;

        private void Update()
        {
            UpdateMapReferences();
            timer += Time.deltaTime;
            if (timer >= updateInterval)
            {
                if (map != null && map.mapNodeBlocks != null)
                    VisualizeMap();
                timer = 0f;
            }
        }

        private void UpdateMapReferences()
        {
            map = MB_MapsManager.Instance.CurrentSystemMap;
            SystemMapNode = MB_MapsManager.Instance.CurrentSystemMapNode;
            starMap = MB_MapsManager.Instance.CurrentStarMap;
            starNode = MB_MapsManager.Instance.CurrentStarNode;
        }

        public void OnMapGenerated(SystemMap newMap, StarMap newStarMap)
        {
            map = newMap;
            starMap = newStarMap;
            VisualizeMap();
        }

        public void VisualizeMap()
        {
            ClearMapObjects();
            VisualizeSystemNodes(map);
            VisualizeSystemConnections(map);
            VisualizeStarNodes(starMap);
            VisualizeStarConnections(starMap);
        }

        #region System Map Visualization

        private void VisualizeSystemNodes(SystemMap systemMap)
        {
            nodeToObject.Clear();
            for (int blockId = 0; blockId < systemMap.mapNodeBlocks.Count; blockId++)
            {
                var nodeBlock = systemMap.mapNodeBlocks[blockId];
                var positions = CalculateNodePositions(nodeBlock.NodeCount, blockId);

                for (int i = 0; i < nodeBlock.NodeCount; i++)
                {
                    var node = nodeBlock.nodes[i];
                    var nodeObject = InstantiateSystemNode(node, positions[i]);
                    SetNodeColor(nodeObject, node == SystemMapNode);
                    nodeObjects.Add(nodeObject);
                    nodeToObject[node] = nodeObject;
                }
            }
        }

        private GameObject InstantiateSystemNode(SystemMapNode node, Vector3 position)
        {
            var nodeObject = Instantiate(nodePrefab, mapParent);
            nodeObject.name = node.NodeName;
            var mbSystemMapNode = nodeObject.GetComponent<MB_SystemMapNode>();
            if (mbSystemMapNode != null)
                mbSystemMapNode.SetMapNode(node);

            var rectTransform = nodeObject.GetComponent<RectTransform>();
            if (rectTransform != null)
                rectTransform.anchoredPosition = new Vector2(position.x, position.y);

            return nodeObject;
        }

        private void VisualizeSystemConnections(SystemMap systemMap)
        {
            foreach (var connection in systemMap.nodeConnections)
            {
                if (!IsValidSystemConnection(connection)) continue;

                var start = nodeToObject[connection.NodeA].GetComponent<RectTransform>().anchoredPosition;
                var end = nodeToObject[connection.NodeB].GetComponent<RectTransform>().anchoredPosition;
                var lineObj = Instantiate(connectionPrefab, mapParent);
                SetConnectionLine(lineObj, start, end);
                connectionObjects.Add(lineObj);
            }
        }

        private bool IsValidSystemConnection(SystemNodeConnection connection)
        {
            return connection.NodeA != null && connection.NodeB != null &&
                   nodeToObject.ContainsKey(connection.NodeA) &&
                   nodeToObject.ContainsKey(connection.NodeB);
        }

        #endregion

        #region Star Map Visualization

        private void VisualizeStarNodes(StarMap starMap)
        {
            foreach (var starNode in starMap.StarNodes)
            {
                if (starNode == null) continue;
                var starNodeObject = Instantiate(starNodePrefab, starMapParent);
                starNodeObject.name = starNode.NodeName;
                var mbStarMapNode = starNodeObject.GetComponent<MB_StarMapNode>();
                if (mbStarMapNode != null)
                    mbStarMapNode.SetStarNode(starNode);

                var rectTransform = starNodeObject.GetComponent<RectTransform>();
                if (rectTransform != null)
                    rectTransform.anchoredPosition = GetStarNodePosition(starNode, rectTransform);

                SetNodeColor(starNodeObject, starNode == this.starNode);
                nodeObjects.Add(starNodeObject);
            }
        }

        private void VisualizeStarConnections(StarMap starMap)
        {
            var drawnConnections = new HashSet<string>();
            foreach (var starNode in starMap.StarNodes)
            {
                if (starNode == null) continue;
                foreach (var connection in starNode.StarNodeConnections)
                {
                    if (!IsValidStarConnection(connection)) continue;
                    string key = GetConnectionKey(connection.NodeA, connection.NodeB);
                    if (drawnConnections.Contains(key) || drawnConnections.Contains(GetConnectionKey(connection.NodeB, connection.NodeA)))
                        continue;
                    drawnConnections.Add(key);

                    var startRaw = GetStarNodePosition(connection.NodeA, starMapParent.GetComponent<RectTransform>()) + starConnectionOffset;
                    var endRaw = GetStarNodePosition(connection.NodeB, starMapParent.GetComponent<RectTransform>()) + starConnectionOffset;
                    var start = new Vector2(startRaw.x * starConnectionScaleX, startRaw.y * starConnectionScaleY);
                    var end = new Vector2(endRaw.x * starConnectionScaleX, endRaw.y * starConnectionScaleY);

                    var lineObj = Instantiate(connectionPrefab, starMapParent);
                    SetStarConnectionLine(lineObj, start, end);
                    connectionObjects.Add(lineObj);
                }
            }
        }

        private bool IsValidStarConnection(StarNodeConnection connection)
        {
            return connection.NodeA != null && connection.NodeB != null;
        }

        private string GetConnectionKey(StarNode a, StarNode b)
        {
            return $"{a.NodeName}_{b.NodeName}";
        }

        #endregion

        #region Utility Methods

        private void SetNodeColor(GameObject nodeObject, bool isSelected)
        {
            var image = nodeObject.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
                image.color = isSelected ? Color.yellow : Color.white;
        }

        private void SetConnectionLine(GameObject lineObj, Vector3 start, Vector3 end)
        {
            var lineRect = lineObj.GetComponent<RectTransform>();
            if (lineRect != null)
            {
                lineRect.anchoredPosition = (start + end) / 2f;
                var dir = end - start;
                float length = dir.magnitude;
                lineRect.sizeDelta = new Vector2(length, lineRect.sizeDelta.y);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                lineRect.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        private void SetStarConnectionLine(GameObject lineObj, Vector2 start, Vector2 end)
        {
            var lineRect = lineObj.GetComponent<RectTransform>();
            if (lineRect != null)
            {
                lineRect.anchoredPosition = (start + end) / 2f - new Vector2(lineRect.sizeDelta.x / 2f, lineRect.sizeDelta.y / 2f);
                var dir = end - start;
                float length = dir.magnitude;
                float avgScale = (starConnectionScaleX + starConnectionScaleY) / 2f;
                lineRect.sizeDelta = new Vector2(length, starConnectionThickness * avgScale);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                lineRect.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        private void ClearMapObjects()
        {
            foreach (var obj in nodeObjects)
                Destroy(obj);
            nodeObjects.Clear();

            foreach (var obj in connectionObjects)
                Destroy(obj);
            connectionObjects.Clear();
        }

        private List<Vector3> CalculateNodePositions(int nodeCount, int nodeBlockId)
        {
            var positions = new List<Vector3>();
            if (nodeCount <= 0) return positions;
            if (nodeCount == 1)
            {
                positions.Add(new Vector3(nodeOffsetX + (nodeBlockId * nodeSpacingX), nodeOffsetY, 0));
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

        private Vector2 GetStarNodePosition(StarNode starNode, RectTransform rectTransform)
        {
            var parentRect = rectTransform.parent as RectTransform;
            if (parentRect == null) return Vector2.zero;

            int gridX = 6, gridY = 4;
            float cellWidth = parentRect.rect.width / gridX;
            float cellHeight = parentRect.rect.height / gridY;

            int x = starNode.ArrayPosition.x, y = starNode.ArrayPosition.y;
            float cellOriginX = x * cellWidth, cellOriginY = y * cellHeight;
            float offsetX = starNode.PositionInCell.x * cellWidth, offsetY = starNode.PositionInCell.y * cellHeight;

            return new Vector2(cellOriginX + offsetX, cellOriginY + offsetY);
        }

        #endregion
    }
}