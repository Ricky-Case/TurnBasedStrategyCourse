using System;
using StringLibrary;
using UnityEngine;

namespace Units
{
    public class TurnSystem : MonoBehaviour
    {
        // Singleton
        //*********
        public static TurnSystem Instance { get; private set; }
        //*********
        
        
        private int _turnNumber = 1;
        private bool _isPlayerTurn = true;
        
        
        // Delegates
        public event EventHandler OnTurnChanged;
        
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError(Errors.InstanceExists + transform + GeneralStrings.Dash + Instance);
                Debug.LogWarning(Warnings.DeletingExtraInstance);
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//

        public void EndTurn()
        {
            _turnNumber++;
            _isPlayerTurn = !_isPlayerTurn;
            
            OnTurnChanged?.Invoke(this, EventArgs.Empty);
        }
        
        
        //*****************//
        //**** GETTERS ****//
        //*****************//
        
        public int GetTurnNumber() =>
            _turnNumber;
        
        public bool IsPlayerTurn() =>
            _isPlayerTurn;
    }
}