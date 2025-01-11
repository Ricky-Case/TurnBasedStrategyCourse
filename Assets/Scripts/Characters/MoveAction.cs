using System.Collections.Generic;
using Grid;
using UnityEngine;

namespace Characters
{
    public class MoveAction : MonoBehaviour
    {
        [SerializeField] private Animator unitAnimator;
        [SerializeField] private int maxMoveDistance = 4;
        
        private Vector3 _targetPosition;
        private Unit _unit;
        
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");
        
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        private void Awake()
        {
            _unit = GetComponent<Unit>();
            _targetPosition = transform.position;
        }

        private void Update()
        {
            float stoppingDistance = 0.05f;
            
            if (Vector3.Distance(transform.position, _targetPosition) <= stoppingDistance)
            {
                unitAnimator.SetBool(IsWalking, false);
                return;
            }
            
            Vector3 moveDirection = (_targetPosition - transform.position).normalized;
            float moveSpeed = 4.0f;
            float rotateSpeed = 10.0f;
            
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
            
            unitAnimator.SetBool(IsWalking, true);
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//

        public void Move(GridPosition targetPosition) { _targetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition); }

        public bool IsValidActionGridPosition(GridPosition gridPosition) =>
            GetValidActionGridPositionList().Contains(gridPosition);
        
        //*****************//
        //**** GETTERS ****//
        //*****************//

        public List<GridPosition> GetValidActionGridPositionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = _unit.GetGridPosition();
            
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