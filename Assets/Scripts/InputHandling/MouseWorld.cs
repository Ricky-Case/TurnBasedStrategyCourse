using StringLibrary;
using UnityEngine;

namespace InputHandling
{
    public class MouseWorld : MonoBehaviour
    {
        [SerializeField] private LayerMask mousePlaneLayerMask;
        
        private static MouseWorld _instance;

        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//
        
        private void Awake() { _instance = this; }

        
        //*****************//
        //**** GETTERS ****//
        //*****************//
        
        public static Vector3 GetPosition()
        {
            Camera mainCamera = Camera.main;
            
            if (!mainCamera)
            {
                Debug.Log(Errors.NoMainCamera);
                return Vector3.zero;
            }
            
            Physics.Raycast(
                mainCamera.ScreenPointToRay(Input.mousePosition),
                out RaycastHit hit,
                float.MaxValue,
                _instance.mousePlaneLayerMask);

            return hit.point;
        }
    }
}