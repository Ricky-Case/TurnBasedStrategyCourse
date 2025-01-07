using InputHandling;
using UnityEngine;

namespace Characters
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Animator unitAnimator;
        
        private Vector3 _targetPosition;

        private void Awake()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            float stoppingDistance = 0.05f;
            
            if (Vector3.Distance(transform.position, _targetPosition) > stoppingDistance)
            {
                Debug.Log(name + " MOVING");
                Vector3 moveDirection = (_targetPosition - transform.position).normalized;
                float moveSpeed = 4.0f;
                float rotateSpeed = 10.0f;
                
                transform.position += moveDirection * (moveSpeed * Time.deltaTime);
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
                
                unitAnimator.SetBool("IsWalking", true);
            }
            else
            {
                unitAnimator.SetBool("IsWalking", false);
            }
        }
        
        public void Move(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }
    }
}