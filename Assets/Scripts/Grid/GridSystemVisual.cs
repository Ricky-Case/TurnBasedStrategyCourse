using System.Collections.Generic;
using Characters;
using Characters.Actions;
using UnityEngine;

namespace Grid
{
    public class GridSystemVisual : MonoBehaviour
    {
        // Singleton
        //*********
        public static GridSystemVisual Instance { get; private set; }
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
                Debug.LogError("More than one (1) GridSystemVisual! " + transform + " - " + Instance);
                Debug.LogWarning("Deleting extraneous instance!");
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
            ShowGridPositionList(selectedAction.CreateValidActionGridPositionList());
        }
    }
}