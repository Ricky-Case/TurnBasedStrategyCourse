using Characters;
using System;
using UnityEngine;

namespace InputHandling
{
    public class UnitActionSystem : MonoBehaviour
    {
        [SerializeField] private Unit selectedUnit;
        [SerializeField] private LayerMask unitLayerMask;

        public static UnitActionSystem Instance { get; private set; }
        
        public event EventHandler OnSelectedUnitChanged;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one (1) UnitActionSystem! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }
        
        private void Update()
        {
            // TODO: Change this to use the Unity Input System instead.
            if (Input.GetMouseButton(0))
            {
                if (TryHandleUnitSelection()) { return; }
                
                selectedUnit.Move(MouseWorld.GetPosition());
            }
        }

        private bool TryHandleUnitSelection()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitLayerMask))
            {
                if (hit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    SetSelectedUnit(unit);
                    return true;
                }
            }

            return false;
        }

        private void SetSelectedUnit(Unit unit)
        {
            selectedUnit = unit;
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }

        public Unit GetSelectedUnit()
        {
            return selectedUnit;
        }
    }
}