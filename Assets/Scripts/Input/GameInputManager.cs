using System;
using UnityEngine;

namespace Input
{
    public class GameInputManager : MonoBehaviour
    {
        private InputActions _inputActions;

        private void Awake()
        {
            _inputActions ??= new InputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }

        public Vector2 Movement => _inputActions.GameInput.Movement.ReadValue<Vector2>();
        public Vector2 CameraLock => _inputActions.GameInput.CameraLock.ReadValue<Vector2>();

    }
}
