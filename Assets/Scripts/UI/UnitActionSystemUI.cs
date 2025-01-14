using System;
using System.Collections.Generic;
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

        private List<ActionButtonUI> actionButtonUIList;
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        private void Awake()
        {
            actionButtonUIList = new List<ActionButtonUI>();
        }
        
        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
            
            DrawUnitActionButtons();
            UpdateSelectedVisual();
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
                
                actionButtonUIList.Add(actionButtonUI);
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
            
            actionButtonUIList.Clear();
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs eventArgs)
        {
            DrawUnitActionButtons();
            UpdateSelectedVisual();
        }
        
        private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs eventArgs)
        {
            UpdateSelectedVisual();
        }
        
        private void UpdateSelectedVisual()
        {
            foreach (ActionButtonUI actionButtonUI in actionButtonUIList)
            {
                actionButtonUI.UpdateSelectedVisual();
            }
        }
    }
}