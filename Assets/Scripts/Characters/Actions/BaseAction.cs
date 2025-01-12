using System;
using UnityEngine;

namespace Characters.Actions
{
    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit Unit;
        protected bool IsActive;

        
        // Delegates
        protected Action<bool> OnActionCompleted;
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        protected virtual void Awake() { Unit = GetComponent<Unit>(); }
    }
}