using System;
using Unilts.Tools.Singleton;
using UnityEngine;

namespace Input
{
    public class GameInputManager : Singleton<GameInputManager>
    {
        private InputActions _inputActions;

        protected override void Awake()
        {
            base.Awake();
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

        public bool Run => _inputActions.GameInput.Run.triggered;
        
        public bool Climb => _inputActions.GameInput.Climb.triggered;
        
        public bool LAttack => _inputActions.GameInput.LAttack.triggered;
        
        public bool RAttack => _inputActions.GameInput.RAttack.triggered;
        
        public bool Grab => _inputActions.GameInput.Grab.triggered;

        public bool Takeout => _inputActions.GameInput.Takeout.triggered;

    }
}
