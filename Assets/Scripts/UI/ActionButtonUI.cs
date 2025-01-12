using Characters.Actions;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class ActionButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private Button button;
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        public void SetBaseAction(BaseAction baseAction)
        {
            textMeshPro.text = baseAction.GetName().ToUpper();

            button.onClick.AddListener(() => {
                UnitActionSystem.Instance.SetSelectedAction(baseAction);
            });
        }

    }
}