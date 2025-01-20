using System;
using StringLibrary;
using Units.Actions.Combat;
using Units.Actions.Movement;
using UnityEngine;

namespace Animation
{
    public class UnitAnimator : MonoBehaviour
    {
        [SerializeField] Animator animator;
        
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        private void Awake()
        {
            if (TryGetComponent(out MoveAction moveAction))
            {
                moveAction.OnStartMoving += MoveAction_OnStartMoving;
                moveAction.OnStopMoving += MoveAction_OnStopMoving;
            }

            if (TryGetComponent(out ShootAction shootAction))
            {
                shootAction.OnShoot += ShootAction_OnShoot;
            }
        }


        //********************************//
        //**** CUSTOM EVENT FUNCTIONS ****//
        //********************************//
        
        private void MoveAction_OnStartMoving(object sender, EventArgs eventArgs)
        {
            animator.SetBool(AnimationIDs.Move, true);
        }
        
        private void MoveAction_OnStopMoving(object sender, EventArgs eventArgs)
        {
            animator.SetBool(AnimationIDs.Move, false);
        }

        private void ShootAction_OnShoot(object sender, EventArgs eventArgs)
        {
            animator.SetTrigger(AnimationIDs.Shoot);
        }
    }
}