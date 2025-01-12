using System;
using Characters;
using Characters.Actions;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonContainer;
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//
        
        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            DrawUnitActionButtons();
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        private void CreateUnitActionButtons()
        {
            Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

            foreach (BaseAction baseAction in selectedUnit.GetBaseActions())
            {
                Transform actionButton = Instantiate(actionButtonPrefab, actionButtonContainer);
                ActionButtonUI actionButtonUI = actionButton.GetComponent<ActionButtonUI>();
                actionButtonUI.SetBaseAction(baseAction);
            }
        }

        private void DrawUnitActionButtons()
        {
            RemoveUnitActionButtons();
            CreateUnitActionButtons();
        }

        private void RemoveUnitActionButtons()
        {
            foreach (Transform actionButton in actionButtonContainer)
            {
                Destroy(actionButton.gameObject);
            }
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs eventArgs)
        {
            DrawUnitActionButtons();
        }
    }
}