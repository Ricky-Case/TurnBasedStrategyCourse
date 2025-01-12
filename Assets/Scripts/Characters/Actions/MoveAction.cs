using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Characters.Actions
{
    public class MoveAction : BaseAction
    {
        [Header("ANIMATION")]
        [SerializeField] private Animator unitAnimator;
        
        private static readonly int AnimMoveID = Animator.StringToHash("Move");
        
        
        [Header("MOVEMENT")]
        [SerializeField] private int maxMoveDistance = 4;
        [SerializeField] private float moveSpeed = 4.0f;
        [SerializeField] private float rotateSpeed = 10.0f;
        
        private Vector3 _targetPosition;
        
        
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
            
            float stoppingDistance = 0.01f;
            
            if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
            {
                transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            }
            else
            {
                IsActive = false;
                OnActionCompleted(IsActive);
            }
            
            unitAnimator.SetBool(AnimMoveID, IsActive);
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//

        public void Move(GridPosition targetPosition, Action<bool> actionCompletedDelegate)
        {
            _targetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
            OnActionCompleted = actionCompletedDelegate;
            IsActive = true;
            OnActionCompleted(IsActive);
        }

        public bool IsValidActionGridPosition(GridPosition gridPosition) =>
            GetValidActionGridPositionList().Contains(gridPosition);
        
        //*****************//
        //**** GETTERS ****//
        //*****************//

        public override string GetName() =>
            "MOVE";

        public List<GridPosition> GetValidActionGridPositionList()
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
                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) { continue; }
                    
                    validGridPositionList.Add(testGridPosition);
                }
            }

            return validGridPositionList;
        }
    }
}