using InputHandling;
using System;
using Grid;
using StringLibrary;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Units.Actions
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
        private Camera _mainCamera;
        
        
        // Delegates
        public event EventHandler OnActionStarted;
        public event EventHandler<bool> OnBusyChanged;
        public event EventHandler OnSelectedActionChanged;
        public event EventHandler OnSelectedUnitChanged;

        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError(Errors.InstanceExists + transform + GeneralStrings.Dash + Instance);
                Debug.LogWarning(Warnings.DeletingExtraInstance);
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        private void Start()
        {
            _mainCamera = Camera.main;
            SetSelectedUnit(selectedUnit);
        }
        
        private void Update()
        {
            if (_isBusy) { return; }
            if (!TurnSystem.Instance.IsPlayerTurn()) { return; }
            if (EventSystem.current.IsPointerOverGameObject()) { return; }
            if (TryHandleUnitSelection()) { return; }
            
            HandleSelectedAction();
        }

        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//

        private void HandleSelectedAction()
        {
            // TODO: Change this to use the Unity Input System instead.
            if (!Input.GetMouseButtonDown(0)) { return; }

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (!_selectedAction.IsValidActionGridPosition(mouseGridPosition)) { return; }
            if (!selectedUnit.TryTakeAction(_selectedAction)) { return; }

            _selectedAction.TakeAction(mouseGridPosition, SetIsBusy);
            OnActionStarted?.Invoke(this, EventArgs.Empty);
        }

        private bool TryHandleUnitSelection()
        {
            // TODO: Change this to use the Unity Input System instead.
            if (!Input.GetMouseButtonDown(0)) { return false; }

            if (!Physics.Raycast(
                    _mainCamera.ScreenPointToRay(Input.mousePosition),
                    out RaycastHit hit,
                    float.MaxValue,
                    unitLayerMask))
            { return false; }

            if (!hit.transform.TryGetComponent(out Unit unit)) { return false; }
            if (unit == selectedUnit) { return false; }
            if (unit.IsEnemy()) { return false; }
                    
            SetSelectedUnit(unit);
            return true;
        }

        
        //*****************//
        //**** GETTERS ****//
        //*****************//
        
        public Unit GetSelectedUnit() =>
            selectedUnit;
        
        public BaseAction GetSelectedAction() =>
            _selectedAction;
        
        
        //*****************//
        //**** SETTERS ****//
        //*****************//

        private void SetIsBusy(bool isBusy)
        {
            _isBusy = isBusy;
            OnBusyChanged?.Invoke(this, _isBusy);
        }

        public void SetSelectedAction(BaseAction baseAction)
        {
            _selectedAction = baseAction;
            OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
        }
        
        private void SetSelectedUnit(Unit unit)
        {
            selectedUnit = unit;
            _selectedAction = unit.GetMoveAction();
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}