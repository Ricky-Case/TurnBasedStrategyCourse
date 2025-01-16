using System;
using System.Collections.Generic;
using Characters;
using Characters.Actions;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UnitActionSystemUI : MonoBehaviour
    {
        [SerializeField] private Transform actionButtonPrefab;
        [SerializeField] private Transform actionButtonContainer;
        [SerializeField] private TextMeshProUGUI actionPointsText;

        private List<ActionButtonUI> _actionButtonUIList;
        
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        private void Awake()
        {
            _actionButtonUIList = new List<ActionButtonUI>();
        }
        
        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionSystem_OnSelectedActionChanged;
            UnitActionSystem.Instance.OnActionStarted += UnitActionSystem_OnActionStarted;
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
            Unit.OnActionPointsChanged += Unit_OnActionPointsChanged;
            
            DrawUnitActionButtons();
            UpdateSelectedVisual();
            UpdateActionPoints();
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
                
                _actionButtonUIList.Add(actionButtonUI);
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
            
            _actionButtonUIList.Clear();
        }

        private void UpdateActionPoints()
        {
            actionPointsText.text = "ACTION POINTS: " + UnitActionSystem.Instance.GetSelectedUnit().GetActionPoints();
        }
        
        private void UpdateSelectedVisual()
        {
            foreach (ActionButtonUI actionButtonUI in _actionButtonUIList)
            {
                actionButtonUI.UpdateSelectedVisual();
            }
        }
        
        
        //*********************************//
        //**** CUSTOM EVENTS FUNCTIONS ****//
        //*********************************//

        private void Unit_OnActionPointsChanged(object sender, EventArgs eventArgs)
        {
            UpdateActionPoints();
        }
        
        private void UnitActionSystem_OnActionStarted(object sender, EventArgs eventArgs)
        {
            UpdateActionPoints();
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs eventArgs)
        {
            DrawUnitActionButtons();
            UpdateSelectedVisual();
            UpdateActionPoints();
        }
        
        private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs eventArgs)
        {
            UpdateSelectedVisual();
        }

        private void TurnSystem_OnTurnChanged(object sender, EventArgs eventArgs)
        {
            UpdateActionPoints();
        }
    }
}