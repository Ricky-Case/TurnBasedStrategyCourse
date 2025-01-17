using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Grid
{
    public class LevelGrid : MonoBehaviour
    {
        // Singleton
        // *********
        public static LevelGrid Instance { get; private set; }
        //*********
        
        
        [SerializeField] private Transform gridDebugObjectPrefab;
        
        private GridSystem _gridSystem;
        
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one (1) LevelGrid! " + transform + " - " + Instance);
                Debug.LogWarning("Deleting extraneous instance!");
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            
            _gridSystem = new GridSystem(10, 10, 2.0f);
            _gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
            return gridObject.HasAnyUnit();
        }
        
        public bool IsValidGridPosition(GridPosition gridPosition) =>
            _gridSystem.IsValidGridPosition(gridPosition);
        
        public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
        {
            RemoveUnitAtGridPosition(fromGridPosition, unit);
            AddUnitAtGridPosition(toGridPosition, unit);
        }
        
        
        //*****************//
        //**** GETTERS ****//
        //*****************//

        public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition) =>
            _gridSystem.GetGridObject(gridPosition).GetUnitList();
        
        public GridPosition GetGridPosition(Vector3 worldPosition) =>
            _gridSystem.GetGridPosition(worldPosition);
        
        public int GetHeight() =>
            _gridSystem.GetHeight();
        
        public int GetWidth() =>
            _gridSystem.GetWidth();

        public Vector3 GetWorldPosition(GridPosition gridPosition) =>
            _gridSystem.GetWorldPosition(gridPosition);
        
        
        //*****************//
        //**** SETTERS ****//
        //*****************//
        
        public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            // TODO: Save memory by not temporarily storing the gridObject.
            GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
            gridObject.AddUnit(unit);
        }
        
        public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            // TODO: Save memory by not temporarily storing the gridObject.
            GridObject gridObject = _gridSystem.GetGridObject(gridPosition);
            gridObject.RemoveUnit(unit);
        }
    }
}