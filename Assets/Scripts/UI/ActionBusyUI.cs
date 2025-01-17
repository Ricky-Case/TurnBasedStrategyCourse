using Units.Actions;
using UnityEngine;

namespace UI
{
    public class ActionBusyUI : MonoBehaviour
    {
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//
        
        private void Start()
        {
            UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnBusyChanged;
            Show(false);
        }

        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//

        private void UnitActionSystem_OnBusyChanged(object sender, bool isBusy)
        {
            Show(isBusy);
        }
        
        private void Show(bool showVisual)
        {
            gameObject.SetActive(showVisual);
        }
    }
}