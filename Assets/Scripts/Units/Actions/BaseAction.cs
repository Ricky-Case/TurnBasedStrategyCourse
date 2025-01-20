using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Units.Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit Unit;
        protected bool IsActive;
        protected const int BaseCost = 1;

        
        // Delegates
        private Action<bool> _onActionCompleted;
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        protected virtual void Awake() { Unit = GetComponent<Unit>(); }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//

        protected void ActionCompleted()
        {
            IsActive = false;
            _onActionCompleted(IsActive);
            _onActionCompleted?.Invoke(IsActive);
        }

        private void ActionStarted(Action<bool> onActionCompleted)
        {
            _onActionCompleted = onActionCompleted;
            IsActive = true;
            _onActionCompleted(IsActive);
        }
        
        protected abstract List<GridPosition> CreateValidActionGridPositionList();
        
        public bool IsValidActionGridPosition(GridPosition gridPosition) =>
            CreateValidActionGridPositionList().Contains(gridPosition);

        public virtual void TakeAction(GridPosition gridPosition, Action<bool> onActionCompleted)
        {
            ActionStarted(onActionCompleted);
        }
        
        
        //*****************//
        //**** GETTERS ****//
        //*****************//
        
        public abstract string GetName();
        
        public virtual int GetCost() =>
            BaseCost;

        public virtual List<GridPosition> GetValidActionGridPositionList() =>
            CreateValidActionGridPositionList();
    }
}