using Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TurnSystemUI : MonoBehaviour
    {
        [SerializeField] Button endTurnButton;
        [SerializeField] TextMeshProUGUI turnNumberText;
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        private void Start()
        {
            endTurnButton.onClick.AddListener(() =>
            {
                TurnSystem.Instance.EndTurn();
                UpdateTurnNumberText(TurnSystem.Instance.GetTurnNumber());
            });
            
            UpdateTurnNumberText(TurnSystem.Instance.GetTurnNumber());
        }
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        private void UpdateTurnNumberText(int turnNumber) { turnNumberText.text = "TURN " + turnNumber; }
    }
}