using System;
using System.Collections.Generic;
using Grid;
using StringLibrary;
using UnityEngine;

namespace Units.Actions.Combat
{
    public class ShootAction : BaseAttackAction
    {
        [SerializeField] [Range(BaseCost, BaseCost + 5)] private int cost = BaseCost;
        
        private State currentState;
        private const int WeaponDamage = 5;
        
        // Delegates...
        public event EventHandler OnShoot;
        
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//
        
        private void Update()
        {
            if (!IsActive) { return; }

            if (CanAttack && RotateTowardsTarget()) { Shoot(); }
            
            StateTimer -= Time.deltaTime;
            if (StateTimer <= 0.0f) { GoToNextState(); }
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//

        protected override void DealDamage() { TargetUnit.TakeDamage(WeaponDamage); }
        
        protected override List<GridPosition> CreateValidActionGridPositionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = Unit.GetGridPosition();
            
            for (int xIndex = -maxAttackDistance; xIndex <= maxAttackDistance; xIndex++)
            {
                for (int zIndex = -maxAttackDistance; zIndex <= maxAttackDistance; zIndex++)
                {
                    GridPosition offsetGridPosition = new GridPosition(xIndex, zIndex);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    // If grid position is not valid...
                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) { continue; }
                    // If radius to grid position is greater than max allowed distance...
                    if (Mathf.Abs(xIndex) + Mathf.Abs(zIndex) > maxAttackDistance) { continue; }
                    // If grid position does not contain another unit...
                    if (!LevelGrid.Instance.HasAnyUnitAtGridPosition(testGridPosition)) { continue; }

                    Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);
                    
                    // If target unit is friendly...
                    if (targetUnit.IsEnemy() == Unit.IsEnemy()) { continue; }
                    
                    validGridPositionList.Add(testGridPosition);
                }
            }

            return validGridPositionList;
        }

        protected override void GoToNextState()
        {
            switch (currentState)
            {
                case State.Preparing:
                    CanAttack = false;
                    currentState = State.Attacking;
                    ResetStateTimer(attackTime);
                    break;
                case State.Attacking:
                    DealDamage();
                    currentState = State.Cooldown;
                    ResetStateTimer(cooldownTime);
                    break;
                case State.Cooldown:
                    ActionCompleted();
                    break;
            }
        }

        private void Shoot()
        {
            OnShoot?.Invoke(this, EventArgs.Empty);
            CanAttack = false;
        }
        
        
        //*****************//
        //**** GETTERS ****//
        //*****************//

        public override string GetName() =>
            ActionNames.Shoot;

        public override int GetCost() =>
            cost;
    }
}