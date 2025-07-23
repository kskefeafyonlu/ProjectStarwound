using System.Collections.Generic;
using _Project.Code.MapGenerators;
using _Project.Code.MapGenerators.StarMapGeneration;
using _Project.Code.SystemMapGenerator;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Project.Code
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;
        
        public List<GameObject> hitObjs = new List<GameObject>();
        
        public InputActionReference pointerAction;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            if (pointerAction != null)
            {
                pointerAction.action.performed += ctx => CheckNodeHits();
            }
            else
            {
                Debug.LogError("Pointer action reference is not set!");
            }
        }


        private void Update()
        {
            
            if (hitObjs.Count > 0)
            {
                foreach (var obj in hitObjs)
                {
                    Debug.Log("Hit object: " + obj.name);
                }
            }

        }



        public List<GameObject> GetObjectsUnderPointer()
        {

            var hitObjects = new List<GameObject>();

            Vector2 pointerPos = Pointer.current.position.ReadValue();

            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = pointerPos
            };

            // Use the new API to find the GraphicRaycaster
            GraphicRaycaster raycaster = Object.FindFirstObjectByType<GraphicRaycaster>();
            if (raycaster == null)
            {
                Debug.LogError("No GraphicRaycaster found in scene!");
                return hitObjects;
            }

            var results = new List<RaycastResult>();
            raycaster.Raycast(eventData, results);

            foreach (var result in results)
            {
                hitObjects.Add(result.gameObject);
                
            }

            return hitObjects;
        }


        
        public void CheckNodeHits()
        {
            List<GameObject> hitObjs = GetObjectsUnderPointer();
            if (hitObjs.Count > 0)
            {
                foreach (var obj in hitObjs)
                {
                    MB_StarNode starNode = obj.GetComponent<MB_StarNode>();
                    if (starNode != null && starNode.StarNode != null && MB_MapManager.Instance != null)
                    {
                        Debug.Log("Hit Star Node: " + starNode.StarNode.NodeName);
                        MB_MapManager.Instance.SelectStarNode(starNode.StarNode);
                    }

                    MB_MapNode mapNode = obj.GetComponent<MB_MapNode>();
                    if (mapNode != null && mapNode.MapNode != null && MB_MapManager.Instance != null)
                    {
                        Debug.Log("Hit Map Node: " + mapNode.MapNode.NodeName);
                        MB_MapManager.Instance.SelectSystemNode(mapNode.MapNode);
                    }
                }
            }
        }
    }



}