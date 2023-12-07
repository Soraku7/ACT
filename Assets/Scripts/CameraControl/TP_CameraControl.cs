using System;
using Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace CameraControl
{
    public class TP_CameraControl : MonoBehaviour
    {
        private GameInputManager _gameInputManager;
        
        [SerializeField , Header("相机参数")] private float controlSpeed;
        [SerializeField] private Vector2 cameraVerticalMaxAngle;
        [SerializeField] private float smoothSpeed;
        [SerializeField] private Transform lookTarget;
        [SerializeField] private float positionOffset;
        [SerializeField] private float positionSmoothTime;

        private Vector3 _smoothDampVelocity = Vector3.zero;
        private Vector2 _input;
        private Vector3 _cameraRotation;

        private void Awake()
        {
            _gameInputManager = GetComponent<GameInputManager>();
            lookTarget = GameObject.FindWithTag("CameraTarget").transform; 
        }

        private void Update()
        {
            CameraInput();
            
        }

        private void LateUpdate()
        {
            UpdateCameraRotation();
            CameraPosition();
        }

        private void CameraInput()
        {
            _input.y += _gameInputManager.CameraLock.x * controlSpeed;
            _input.x -= _gameInputManager.CameraLock.y * controlSpeed;
            _input.x = Mathf.Clamp(_input.x, cameraVerticalMaxAngle.x, cameraVerticalMaxAngle.y);
        }

        private void UpdateCameraRotation()
        {
            _cameraRotation = Vector3.SmoothDamp(_cameraRotation, new Vector3(_input.x, _input.y, 0)
                , ref _smoothDampVelocity, smoothSpeed);
            transform.eulerAngles = _cameraRotation;
        }

        private void CameraPosition()
        {
            var newPosition= lookTarget.position + (-transform.forward * positionOffset);
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * positionSmoothTime);
        }
    }
}
