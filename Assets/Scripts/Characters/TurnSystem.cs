using System;
using UnityEngine;

namespace Characters
{
    public class TurnSystem : MonoBehaviour
    {
        // Singleton
        //*********
        public static TurnSystem Instance { get; private set; }
        //*********
        
        
        private int _turnNumber = 1;
        
        
        // Delegates
        public event EventHandler OnTurnChanged;
        
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("More than one (1) TurnSystem! " + transform + " - " + Instance);
                Debug.LogWarning("Deleting extraneous instance!");
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
            OnTurnChanged?.Invoke(this, EventArgs.Empty);
        }
        
        
        //*****************//
        //**** GETTERS ****//
        //*****************//
        
        public int GetTurnNumber() =>
            _turnNumber;
    }
}