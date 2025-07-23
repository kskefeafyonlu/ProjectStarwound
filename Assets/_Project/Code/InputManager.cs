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

        public InputActionReference pointerAction;

        private void Awake()
        {
            InitializeSingleton();
            RegisterPointerAction();
        }

        private void Update()
        {
            DebugHitObjects();
        }

        private void InitializeSingleton()
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
        }

        private void RegisterPointerAction()
        {
            if (pointerAction != null)
            {
                pointerAction.action.performed += ctx => OnPointerPerformed();
            }
            else
            {
                Debug.LogError("Pointer action reference is not set!");
            }
        }

        private void OnPointerPerformed()
        {
            HandleNodeSelection();
        }

        private void DebugHitObjects()
        {
            var hitObjects = GetObjectsUnderPointer();
            if (hitObjects.Count > 0)
            {
                foreach (var obj in hitObjects)
                {
                    //Debug.Log("Hit object: " + obj.name);
                }
            }
        }

        private List<GameObject> GetObjectsUnderPointer()
        {
            var hitObjects = new List<GameObject>();
            Vector2 pointerPos = Pointer.current.position.ReadValue();

            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = pointerPos
            };

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

        private void HandleNodeSelection()
        {
            var hitObjects = GetObjectsUnderPointer();
            foreach (var obj in hitObjects)
            {
                TrySelectStarNode(obj);
                TrySelectSystemNode(obj);
            }
        }

        private void TrySelectStarNode(GameObject obj)
        {
            MB_StarMapNode starMapNode = obj.GetComponent<MB_StarMapNode>();
            if (starMapNode != null && starMapNode.StarNode != null && MB_MapsManager.Instance != null)
            {

                MB_MapsManager.Instance.SelectStarNode(starMapNode.StarNode);
            }
        }

        private void TrySelectSystemNode(GameObject obj)
        {
            MB_SystemMapNode systemMapNode = obj.GetComponent<MB_SystemMapNode>();
            if (systemMapNode != null && systemMapNode.SystemMapNode != null && MB_MapsManager.Instance != null)
            {
                MB_MapsManager.Instance.SelectSystemNode(systemMapNode.SystemMapNode);
            }
        }
    }
}