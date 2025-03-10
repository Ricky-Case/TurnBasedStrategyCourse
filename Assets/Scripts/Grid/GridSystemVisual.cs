using System.Collections.Generic;
using StringLibrary;
using Units.Actions;
using UnityEngine;

namespace Grid
{
    public class GridSystemVisual : MonoBehaviour
    {
        // Singleton
        //*********
        private static GridSystemVisual Instance { get; set; }
        //*********
        
        
        [SerializeField] private Transform gridSystemVisualSinglePrefab;
        
        private GridSystemVisualSingle[,] _gridSystemVisualSingles;

        
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
        
        private void Start()
        {
            _gridSystemVisualSingles = new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(),
                                                                  LevelGrid.Instance.GetHeight()];
            
            for (int xIndex = 0; xIndex < LevelGrid.Instance.GetWidth(); xIndex++)
            {
                for (int zIndex = 0; zIndex < LevelGrid.Instance.GetHeight(); zIndex++)
                {
                    Transform gridSystemVisualSingleTransform = Instantiate(
                                gridSystemVisualSinglePrefab, 
                                LevelGrid.Instance.GetWorldPosition(new GridPosition(xIndex, zIndex)), 
                                Quaternion.identity);
                    
                    _gridSystemVisualSingles[xIndex, zIndex] =
                        gridSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
                }
            }
        }

        private void Update()
        {
            UpdateGridVisual();
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//

        public void HideAllGridPositions()
        {
            for (int xIndex = 0; xIndex < LevelGrid.Instance.GetWidth(); xIndex++)
            {
                for (int zIndex = 0; zIndex < LevelGrid.Instance.GetHeight(); zIndex++)
                {
                    _gridSystemVisualSingles[xIndex, zIndex].Hide();
                }
            }
        }

        public void ShowGridPositionList(List<GridPosition> gridPositionList)
        {
            foreach (GridPosition gridPosition in gridPositionList)
            {
                _gridSystemVisualSingles[gridPosition.X, gridPosition.Z].Show();
            }
        }


        public void UpdateGridVisual()
        {
            HideAllGridPositions();
            
            BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
            ShowGridPositionList(selectedAction.GetValidActionGridPositionList());
        }
    }
}