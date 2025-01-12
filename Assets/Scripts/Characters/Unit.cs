using Grid;
using UnityEngine;
using Characters.Actions;

namespace Characters
{
    public class Unit : MonoBehaviour
    {
        private GridPosition _currentGridPosition;
        private MoveAction _moveAction;
        private SpinAction _spinAction;


        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        private void Awake()
        {
            _moveAction = GetComponent<MoveAction>();
            _spinAction = GetComponent<SpinAction>();
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
        public SpinAction GetSpinAction() => _spinAction;
    }
}