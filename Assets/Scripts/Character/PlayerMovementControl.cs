using System;
using Base;
using GGG.Tool;
using Input;
using Manager;
using UnityEngine;

namespace Character
{
    public class PlayerMovementControl : CharacterMovementControlBase
    {
        private float _rotationAngle;
        private float _angleVelocity = 0f;
        [SerializeField]
        private float rotationSmoothTime;

        private Transform _mainCamera;
        
        //脚步声
        private float _nextFootTime;
        [SerializeField] private float _slowFootTime;
        [SerializeField] private float _fastFootTime;
        
        protected override void Awake()
        {
            base.Awake();
            _mainCamera = Camera.main.transform;
        }

        private void LateUpdate()
        {
            CharacterRotation();
            UpdateAnimation();
        }

        private void CharacterRotation()
        {
            if (!CharacterIsOnGround) return;

            if (Anim.GetBool(AnimationID.HasInputID))
            {
                _rotationAngle = Mathf.Atan2(GameInputManager.MainInstance.Movement.x,
                    GameInputManager.MainInstance.Movement.y) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
            }


            if (Anim.GetBool(AnimationID.HasInputID) && Anim.AnimationAtTag("Motion"))
            {
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, _rotationAngle,
                ref _angleVelocity, rotationSmoothTime);
            }
            
        }

        private void UpdateAnimation()
        {
            if (!CharacterIsOnGround) return;
            Anim.SetBool(AnimationID.HasInputID , GameInputManager.MainInstance.Movement != Vector2.zero);
            if (Anim.GetBool(AnimationID.HasInputID))
            {
                if (GameInputManager.MainInstance.Run)
                {
                    Anim.SetBool(AnimationID.RunID , true);
                }
                else
                {
                    Anim.SetBool(AnimationID.RunID , false);
                }
                Anim.SetFloat(AnimationID.MovementID , (Anim.GetBool(AnimationID.RunID)?2f : GameInputManager.MainInstance.Movement.sqrMagnitude) , 0.25f , Time.deltaTime);
            }
            else
            {
                Anim.SetFloat(AnimationID.MovementID , 0f , 0.25f , Time.deltaTime);
                if (Anim.GetFloat(AnimationID.MovementID) < 0.2f)
                {
                    Anim.SetBool(AnimationID.RunID , false);
                }
            }

            PlayCharacterFootSound();
        }

        /// <summary>
        /// 脚步声播放
        /// </summary>
        private void PlayCharacterFootSound()
        {
            if (CharacterIsOnGround && Anim.GetFloat(AnimationID.MovementID) > 0.5f && Anim.AnimationAtTag("Motion"))
            {
                //角色在地面上&处于移动&确认在移动动画中
                _nextFootTime -= Time.deltaTime;
                if (_nextFootTime < 0f)
                {
                    PlayFootSound();
                }
            }
            else
            {
                _nextFootTime = 0f;
            }
        }

        private void PlayFootSound()
        {
            GamePoolManager.MainInstance.TryGetPoolItem("FootSound" , transform.position , Quaternion.identity);
            _nextFootTime = (Anim.GetFloat(AnimationID.MovementID) > 1.1f) ? _fastFootTime : _slowFootTime;
        }
    }
}
