using System;
using UnityEngine;

namespace Characters.Actions
{
    public class SpinAction : BaseAction
    {
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
            
            IsActive = false;
            OnActionCompleted(IsActive);
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//

        public void Spin(Action<bool> actionCompletedDelegate)
        {
            _amountSpun = 0.0f;
            OnActionCompleted = actionCompletedDelegate;
            IsActive = true;
            OnActionCompleted(IsActive);
        }
    }
}