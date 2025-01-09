using Grid;
using UnityEngine;

namespace Characters
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Animator unitAnimator;
        
        private Vector3 _targetPosition;
        private GridPosition _currentGridPosition;
        
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//
        private void Awake() { _targetPosition = transform.position; }

        private void Start()
        {
            _currentGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(_currentGridPosition, this);
        }

        private void Update()
        {
            float stoppingDistance = 0.05f;
            
            if (Vector3.Distance(transform.position, _targetPosition) <= stoppingDistance)
            {
                unitAnimator.SetBool(IsWalking, false);
                return;
            }
            
            Vector3 moveDirection = (_targetPosition - transform.position).normalized;
            float moveSpeed = 4.0f;
            float rotateSpeed = 10.0f;
            
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
            
            unitAnimator.SetBool(IsWalking, true);
            
            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

            if (newGridPosition == _currentGridPosition) { return; }
            
            LevelGrid.Instance.UnitMovedGridPosition(this, _currentGridPosition, newGridPosition);
            _currentGridPosition = newGridPosition;
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//
        
        public void Move(Vector3 targetPosition) { _targetPosition = targetPosition; }
    }
}