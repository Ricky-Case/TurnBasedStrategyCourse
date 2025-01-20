using System;
using Grid;
using UnityEngine;

namespace Units.Actions.Combat
{
    public abstract class BaseAttackAction : BaseAction
    {
        [SerializeField] protected int maxAttackDistance = 4;
        
        [SerializeField] protected float attackTime = 0.1f;
        [SerializeField] protected float cooldownTime = 0.5f;
        [SerializeField] protected float prepareTime = 1.0f;

        [SerializeField] protected float rotateSpeed = 10.0f;

        protected Unit TargetUnit;
        protected bool CanAttack;
        protected float StateTimer;
        protected State CurrentState;

        protected enum State
        {
            Attacking,
            Cooldown,
            Preparing
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//

        protected abstract void DealDamage();
        
        protected abstract void GoToNextState();

        protected void ResetStateTimer(float stateTime) { StateTimer = stateTime; }
        
        protected bool RotateTowardsTarget()
        {
            Vector3 newRotation = (TargetUnit.transform.position - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, newRotation, rotateSpeed * Time.deltaTime);
            
            return (transform.forward == newRotation);
        }
        
        public override void TakeAction(GridPosition targetPosition, Action<bool> onActionCompleted)
        {
            base.TakeAction(targetPosition, onActionCompleted);

            TargetUnit = LevelGrid.Instance.GetUnitAtGridPosition(targetPosition);
            
            CurrentState = State.Preparing;
            ResetStateTimer(prepareTime);

            CanAttack = true;
        }
    }
}