using System;
using Grid;
using UnityEngine;
using Characters.Actions;

namespace Characters
{
    public class Unit : MonoBehaviour
    {
        private const int ActionPointsMax = 2;
        
        private GridPosition _currentGridPosition;
        private MoveAction _moveAction;
        private SpinAction _spinAction;
        private BaseAction[] _baseActions; 
        private int _actionPoints = ActionPointsMax;
        
        
        // Delegates
        public static event EventHandler OnActionPointsChanged;


        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        private void Awake()
        {
            _moveAction = GetComponent<MoveAction>();
            _spinAction = GetComponent<SpinAction>();
            _baseActions = GetComponents<BaseAction>();
        }

        private void Start()
        {
            _currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(_currentGridPosition, this);

            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        }

        private void Update()
        {
            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

            if (newGridPosition == _currentGridPosition) { return; }

            LevelGrid.Instance.UnitMovedGridPosition(this, _currentGridPosition, newGridPosition);
            _currentGridPosition = newGridPosition;
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//

        private bool CanTakeAction(BaseAction actionToTest) { return _actionPoints >= actionToTest.GetCost(); }

        private void SpendActionPoints(int amount) { SetActionPoints(_actionPoints - amount); }
        
        public bool TryTakeAction(BaseAction actionToTry)
        {
            if (!CanTakeAction(actionToTry)) { return false; }
            
            SpendActionPoints(actionToTry.GetCost());
            return true;
        }


        //*****************//
        //**** GETTERS ****//
        //*****************//
        
        public int GetActionPoints() =>
            _actionPoints;
        
        public BaseAction[] GetBaseActions() =>
            _baseActions;
        
        public GridPosition GetGridPosition() =>
            _currentGridPosition;
        
        public MoveAction GetMoveAction() =>
            _moveAction;
        
        public SpinAction GetSpinAction() =>
            _spinAction;
        
        
        //*****************//
        //**** SETTERS ****//
        //*****************//
        
        private void SetActionPoints(int amount)
        {
            _actionPoints = Mathf.Clamp(amount, 0, ActionPointsMax);
            OnActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }
        
        
        //********************************//
        //**** CUSTOM EVENT FUNCTIONS ****//
        //********************************//

        private void TurnSystem_OnTurnChanged(object sender, EventArgs eventArgs)
        {
            SetActionPoints(ActionPointsMax);
        }
    }
}