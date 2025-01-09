using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera virtualCamera;

        private const float MinFollowOffsetY = 1.5f;
        private const float MaxFollowOffsetY = 7.0f;
        private const float MinFollowOffsetZ = -10.0f;
        private const float MaxFollowOffsetZ = -4.5f;

        private CinemachineFollow _cinemachineFollow;
        private Vector3 _targetFollowOffset;
        
        //*******************************//
        //**** UNITY EVENT FUNCTIONS ****//
        //*******************************//

        private void Start()
        {
            _cinemachineFollow = virtualCamera.GetComponent<CinemachineFollow>();
            _targetFollowOffset = _cinemachineFollow.FollowOffset;
        }
        
        private void Update()
        {
            MoveCamera();
            RotateCamera();
            ZoomCamera();
        }
        
        
        //**************************//
        //**** HELPER FUNCTIONS ****//
        //**************************//

        private void MoveCamera()
        {
            Vector3 inputMoveDirection = new Vector3(0.0f, 0.0f, 0.0f);

            // TODO: Edit to use the Unity Input System
            if (Input.GetKey(KeyCode.W)) { inputMoveDirection.z = 1.0f; }
            if (Input.GetKey(KeyCode.A)) { inputMoveDirection.x = -1.0f; }
            if (Input.GetKey(KeyCode.S)) { inputMoveDirection.z = -1.0f; }
            if (Input.GetKey(KeyCode.D)) { inputMoveDirection.x = 1.0f; }

            float moveSpeed = 10.0f;
            
            Vector3 moveVector =
                (transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x).normalized;
            
            transform.position += moveVector * (moveSpeed * Time.deltaTime);
        }

        private void RotateCamera()
        {
            Vector3 rotationVector = new Vector3(0, 0, 0);

            // TODO: Edit to use the Unity Input System
            if (Input.GetKey(KeyCode.Q)) { rotationVector.y = -1.0f; }
            if (Input.GetKey(KeyCode.E)) { rotationVector.y = 1.0f; }

            float rotationSpeed = 100.0f;
            transform.eulerAngles += rotationVector * (rotationSpeed * Time.deltaTime);
        }

        private void ZoomCamera()
        {
            // TODO: Edit to use the Unity Input System
            float scrollDelta = Input.mouseScrollDelta.y;

            if (scrollDelta == 0) { return; }
            
            float zoomSpeed = 75.0f;
            
            _targetFollowOffset.y = Mathf.Clamp(_targetFollowOffset.y - scrollDelta, MinFollowOffsetY, MaxFollowOffsetY);
            _targetFollowOffset.z = Mathf.Clamp(_targetFollowOffset.z + scrollDelta, MinFollowOffsetZ, MaxFollowOffsetZ);
            
            _cinemachineFollow.FollowOffset =
                Vector3.Lerp(_cinemachineFollow.FollowOffset, _targetFollowOffset, Time.deltaTime * zoomSpeed);
        }
    }
}