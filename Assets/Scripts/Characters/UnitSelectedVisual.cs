using System;
using Characters.Actions;
using UnityEngine;

namespace Characters
{
    public class UnitSelectedVisual : MonoBehaviour
    {
        [SerializeField] private Unit unit;
        
        private MeshRenderer _meshRenderer;

        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//
        private void Awake() { _meshRenderer = GetComponent<MeshRenderer>(); }

        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            UpdateVisual();
        }

        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty) { UpdateVisual(); }

        private void UpdateVisual()
        {
            if (UnitActionSystem.Instance.GetSelectedUnit() == unit)
            {
                gameObject.SetActive(true);
                return;
            }
            
            gameObject.SetActive(false);
        }
    }
}