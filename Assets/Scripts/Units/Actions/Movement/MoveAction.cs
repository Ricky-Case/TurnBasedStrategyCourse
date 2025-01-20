using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using StringLibrary;

namespace Units.Actions.Movement
{
    public class MoveAction : BaseAction
    {
        [Header(EditorLabels.Movement)]
        [SerializeField] [Range(BaseCost, BaseCost + 5)] private int cost = BaseCost;
        [SerializeField] private int maxMoveDistance = 4;
        [SerializeField] private float moveSpeed = 4.0f;
        [SerializeField] private float rotateSpeed = 10.0f;
        
        private Vector3 _targetPosition;
        
        // Delegates...
        public event EventHandler OnStartMoving;
        public event EventHandler OnStopMoving;
        
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        protected override void Awake()
        {
            base.Awake();
            _targetPosition = transform.position;
        }

        private void Update()
        {
            if (!IsActive) { return; }
            
            Vector3 moveDirection = (_targetPosition - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
            
            float stoppingDistance = 0.05f;
            
            if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
            {
                transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            }
            else
            {
                OnStopMoving?.Invoke(this, EventArgs.Empty);
                ActionCompleted();
            }
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        protected override List<GridPosition> CreateValidActionGridPositionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = Unit.GetGridPosition();
            
            for (int xIndex = -maxMoveDistance; xIndex <= maxMoveDistance; xIndex++)
            {
                for (int zIndex = -maxMoveDistance; zIndex <= maxMoveDistance; zIndex++)
                {
                    GridPosition offsetGridPosition = new GridPosition(xIndex, zIndex);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    // If grid position is not valid...
                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) { continue; }
                    // If unit is already at grid position...
                    if (unitGridPosition == testGridPosition) { continue; }
                    // If grid position already contains another unit...
                    if (LevelGrid.Instance.HasAnyUnitAtGridPosition(testGridPosition)) { continue; }
                    
                    validGridPositionList.Add(testGridPosition);
                }
            }

            return validGridPositionList;
        }

        public override void TakeAction(GridPosition targetPosition, Action<bool> onActionCompleted)
        {
            base.TakeAction(targetPosition, onActionCompleted);
            _targetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
            OnStartMoving?.Invoke(this, EventArgs.Empty);
        }
        
        //*****************//
        //**** GETTERS ****//
        //*****************//

        public override string GetName() =>
            ActionNames.Move;

        public override int GetCost() =>
            cost;
    }
}