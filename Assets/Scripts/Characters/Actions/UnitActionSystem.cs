using InputHandling;
using System;
using Grid;
using UnityEngine;

namespace Characters.Actions
{
    public class UnitActionSystem : MonoBehaviour
    {
        // Singleton
        //*********
        public static UnitActionSystem Instance { get; private set; }
        //*********
        
        
        [SerializeField] private Unit selectedUnit;
        [SerializeField] private LayerMask unitLayerMask;

        private bool _isBusy;
        private BaseAction _selectedAction;
        
        
        // Delegates
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

        private void Start()
        {
            SetSelectedUnit(selectedUnit);
        }
        
        private void Update()
        {
            if (_isBusy) { return; }
            if (TryHandleUnitSelection()) { return; }
            
            HandleSelectedAction();
        }

        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        private void HandleSelectedAction()
        {
            // TODO: Change this to use the Unity Input System instead.
            if (Input.GetMouseButtonDown(0))
            {
                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
                
                switch (_selectedAction)
                {
                    case MoveAction moveAction:
                        if (moveAction.IsValidActionGridPosition(mouseGridPosition))
                        {
                            moveAction.Move(mouseGridPosition, SetIsBusy);
                        }
                        break;
                    case SpinAction spinAction:
                        spinAction.Spin(SetIsBusy);
                        break;
                }
            }
        }
        
        private bool TryHandleUnitSelection()
        {
            // TODO: Change this to use the Unity Input System instead.
            if (!Input.GetMouseButtonDown(0)) { return false; }
            
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
        
        private void SetIsBusy(bool isBusy) =>
            _isBusy = isBusy;

        public void SetSelectedAction(BaseAction baseAction)
        {
            _selectedAction = baseAction;
        }
        
        private void SetSelectedUnit(Unit unit)
        {
            selectedUnit = unit;
            _selectedAction = unit.GetMoveAction();
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}