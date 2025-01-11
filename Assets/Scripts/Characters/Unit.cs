using Grid;
using UnityEngine;

namespace Characters
{
    public class Unit : MonoBehaviour
    {
        private GridPosition _currentGridPosition;
        private MoveAction _moveAction;


        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        private void Awake()
        {
            _moveAction = GetComponent<MoveAction>();
        }

        private void Start()
        {
            _currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(_currentGridPosition, this);
        }

        private void Update()
        {
            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

            if (newGridPosition == _currentGridPosition)
            {
                return;
            }

            LevelGrid.Instance.UnitMovedGridPosition(this, _currentGridPosition, newGridPosition);
            _currentGridPosition = newGridPosition;
        }

        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        public GridPosition GetGridPosition() => _currentGridPosition;
        public MoveAction GetMoveAction() => _moveAction;
    }
}