using System;
using UnityEngine;

namespace Units.AI
{
    public class EnemyAI : MonoBehaviour
    {
        private float _timer = 0.0f;
        
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        private void Start()
        {
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        }
        
        private void Update()
        {
            if (TurnSystem.Instance.IsPlayerTurn()) { return; }
            
            _timer -= Time.deltaTime;
            if (_timer <= 0.0f)
            {
                TurnSystem.Instance.EndTurn();
            }
        }
        
        
        //********************************//
        //**** CUSTOM EVENT FUNCTIONS ****//
        //********************************//
        
        private void TurnSystem_OnTurnChanged(object sender, System.EventArgs eventArgs)
        {
            _timer = 2.0f;
        }
    }
}