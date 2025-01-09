using TMPro;
using UnityEngine;

namespace Grid
{
    public class GridDebugObject : MonoBehaviour
    {
        [SerializeField] private TextMeshPro positionLabel;
        
        private GridObject _gridObject;

        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//
        private void Update() { positionLabel.text = _gridObject.ToString(); }
        
        
        //*****************//
        //**** GETTERS ****//
        //*****************//
        
        public GridObject GetGridObject() =>
            _gridObject;
        
        
        //*****************//
        //**** SETTERS ****//
        //*****************//
        
        public void SetGridObject(GridObject gridObject) { _gridObject = gridObject; }
    }
}