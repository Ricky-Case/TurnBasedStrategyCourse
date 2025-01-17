using Units.Actions;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UI
{
    public class ActionButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private Button button;
        [SerializeField] private GameObject selectedGameObject;

        private BaseAction _baseAction;
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        public void SetBaseAction(BaseAction baseAction)
        {
            _baseAction = baseAction;
            textMeshPro.text = baseAction.GetName().ToUpper();

            button.onClick.AddListener(() => {
                UnitActionSystem.Instance.SetSelectedAction(baseAction);
            });
        }

        public void UpdateSelectedVisual()
        {
            selectedGameObject.SetActive(UnitActionSystem.Instance.GetSelectedAction() == _baseAction);
        }

    }
}