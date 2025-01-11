using Characters;
using System;
using Grid;
using UnityEngine;

namespace InputHandling
{
    public class UnitActionSystem : MonoBehaviour
    {
        // Singleton
        //*********
        public static UnitActionSystem Instance { get; private set; }
        //*********
        
        
        [SerializeField] private Unit selectedUnit;
        [SerializeField] private LayerMask unitLayerMask;
        
        public event EventHandler OnSelectedUnitChanged;

        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one (1) UnitActionSystem! " + transform + " - " + Instance);
                Debug.LogWarning("Deleting extraneous instance!");
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }
        
        private void Update()
        {
            // TODO: Change this to use the Unity Input System instead.
            if (Input.GetMouseButtonDown(0))
            {
                if (TryHandleUnitSelection()) { return; }

                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

                if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
                {
                    selectedUnit.GetMoveAction().Move(mouseGridPosition);
                }
            }
        }

        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        private bool TryHandleUnitSelection()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitLayerMask))
            {
                if (hit.transform.TryGetComponent(out Unit unit))
                {
                    SetSelectedUnit(unit);
                    return true;
                }
            }

            return false;
        }

        
        //*****************//
        //**** GETTERS ****//
        //*****************//
        
        public Unit GetSelectedUnit() =>
            selectedUnit;
        
        
        //*****************//
        //**** SETTERS ****//
        //*****************//
        
        private void SetSelectedUnit(Unit unit)
        {
            selectedUnit = unit;
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}