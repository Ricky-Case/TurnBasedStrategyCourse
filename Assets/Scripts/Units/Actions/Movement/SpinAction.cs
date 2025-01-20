using System;
using System.Collections.Generic;
using Grid;
using StringLibrary;
using UnityEngine;

namespace Units.Actions.Movement
{
    public class SpinAction : BaseAction
    {
        [SerializeField] [Range(BaseCost, BaseCost + 5)] private int cost = BaseCost;
        
        private float _amountSpun;
        
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//
        
        private void Update()
        {
            if (!IsActive) { return; }

            float spinAmount = 360.0f;
            float spinAddAmount = 360.0f * Time.deltaTime;
            transform.eulerAngles += new Vector3(0.0f, spinAddAmount, 0.0f);
            _amountSpun += spinAddAmount;

            if (_amountSpun < spinAmount) { return; }
            
            ActionCompleted();
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//

        protected override List<GridPosition> CreateValidActionGridPositionList()
        {
            GridPosition unitGridPosition = Unit.GetGridPosition();
            
            return new List<GridPosition> { unitGridPosition };
        }

        public override void TakeAction(GridPosition targetPosition, Action<bool> onActionCompleted)
        {
            base.TakeAction(targetPosition, onActionCompleted);
            _amountSpun = 0.0f;
        }
        
        
        //*****************//
        //**** GETTERS ****//
        //*****************//

        public override string GetName() =>
            ActionNames.Spin;

        public override int GetCost() =>
            cost;
    }
}