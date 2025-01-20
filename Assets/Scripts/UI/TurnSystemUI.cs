using StringLibrary;
using Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TurnSystemUI : MonoBehaviour
    {
        [SerializeField] private Button endTurnButton;
        [SerializeField] private TextMeshProUGUI turnNumberText;
        [SerializeField] private GameObject enemyTurnVisual;
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        private void Start()
        {
            endTurnButton.onClick.AddListener(() => { TurnSystem.Instance.EndTurn(); });

            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
            
            UpdateTurnNumberText(TurnSystem.Instance.GetTurnNumber());
            CheckForEnemyTurn(!TurnSystem.Instance.IsPlayerTurn());
        }
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        private void CheckForEnemyTurn(bool isEnemyTurn)
        {
            endTurnButton.gameObject.SetActive(!isEnemyTurn);
            enemyTurnVisual.SetActive(isEnemyTurn);
        }
        
        private void UpdateTurnNumberText(int turnNumber) { turnNumberText.text = UIStrings.TurnLabel + turnNumber; }
        
        // Commented out and replaced with the CheckForEnemyTurn function to reduce redundency.
        // Keeping in case I need to change back.
        // private void UpdateEndTurnButtonVisibility()
        // {
        //     endTurnButton.gameObject.SetActive(TurnSystem.Instance.IsPlayerTurn());
        // }
        //
        // private void UpdateEnemyTurnVisual()
        // {
        //     enemyTurnVisual.SetActive(!TurnSystem.Instance.IsPlayerTurn());
        // }
        
        
        //********************************//
        //**** CUSTOM EVENT FUNCTIONS ****//
        //********************************//
        
        private void TurnSystem_OnTurnChanged(object sender, System.EventArgs eventArgs)
        {
            UpdateTurnNumberText(TurnSystem.Instance.GetTurnNumber());
            CheckForEnemyTurn(!TurnSystem.Instance.IsPlayerTurn());
        }
    }
}