using UnityEngine;

namespace Grid
{
    public class GridSystem
    {
        private readonly float _cellSize;
        private readonly int _height;
        private readonly int _width;
        
        private readonly GridObject[,] _gridObjects;
        
        
        //**********************//
        //**** CONSTRUCTORS ****//
        //**********************//
        
        public GridSystem(int width, int height, float cellSize)
        {
            _cellSize = cellSize;
            _height = height;   // TODO: Change to "_length" once tutorial is complete.
            _width = width;
            
            _gridObjects = new GridObject[_width, _height];

            for (int xIndex = 0; xIndex < _width; xIndex++)
            {
                for (int zIndex = 0; zIndex < _height; zIndex++)
                {
                    _gridObjects[xIndex, zIndex] = new GridObject(this, new GridPosition(xIndex, zIndex));
                }
            }
        }

        
        //*****************//
        //**** GETTERS ****//
        //*****************//
        
        public GridObject GetGridObject(GridPosition gridPosition) =>
            _gridObjects[gridPosition.X, gridPosition.Z];

        public GridPosition GetGridPosition(Vector3 worldPosition) => 
            new GridPosition(
                Mathf.RoundToInt(worldPosition.x / _cellSize),
                Mathf.RoundToInt(worldPosition.z / _cellSize)
            );
        
        public int GetHeight() =>
            _height;

        public int GetWidth() =>
            _width;

        public Vector3 GetWorldPosition(GridPosition gridPosition) =>
            new Vector3(gridPosition.X, 0, gridPosition.Z) * _cellSize;
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        public void CreateDebugObjects(Transform debugPrefab)
        {
            for (int xIndex = 0; xIndex < _width; xIndex++)
            {
                for (int zIndex = 0; zIndex < _height; zIndex++)
                {
                    GridPosition gridPosition = new GridPosition(xIndex, zIndex);
                    
                    Transform debugTransform = Object.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                    GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                    gridDebugObject.SetGridObject(GetGridObject(gridPosition));
                }
            }
        }
        
        public bool IsValidGridPosition(GridPosition gridPosition) =>
            gridPosition is { X: >= 0, Z: >= 0 } && gridPosition.X < _width && gridPosition.Z < _height;
    }
}