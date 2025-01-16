using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Characters.Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit Unit;
        protected bool IsActive;
        protected const int BaseCost = 1;

        
        // Delegates
        protected Action<bool> OnActionCompleted;
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        protected virtual void Awake() { Unit = GetComponent<Unit>(); }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        public abstract List<GridPosition> CreateValidActionGridPositionList();
        
        public bool IsValidActionGridPosition(GridPosition gridPosition) =>
            CreateValidActionGridPositionList().Contains(gridPosition);

        public abstract void TakeAction(GridPosition gridPosition, Action<bool> onActionCompleted);
        
        
        //*****************//
        //**** GETTERS ****//
        //*****************//
        
        public abstract string GetName();
        
        public virtual int GetCost() =>
            BaseCost;
    }
}