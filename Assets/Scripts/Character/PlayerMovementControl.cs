using System;
using Base;
using GGG.Tool;
using Input;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public class PlayerMovementControl : CharacterMovementControlBase
    {
        private float _rotationAngle;
        private float _angleVelocity = 0f;
        [SerializeField]
        private float rotationSmoothTime;


        private void LateUpdate()
        {
            CharacterRotation();
            UpdateAnimation();
        }

        private void CharacterRotation()
        {
            if (!CharacterIsOnGround) return;

            if (Anim.GetBool("HasInput"))
            {
                _rotationAngle = Mathf.Atan2(GameInputManager.MainInstance.Movement.x,
                    GameInputManager.MainInstance.Movement.y) * Mathf.Rad2Deg;
            }


            if (Anim.GetBool("HasInput") && Anim.AnimationAtTag("Motion"))
            {
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, _rotationAngle,
                ref _angleVelocity, rotationSmoothTime);
            }
            
        }

        private void UpdateAnimation()
        {
            if (!CharacterIsOnGround) return;
            Debug.Log(Anim.GetBool("HasInput"));
            Anim.SetBool("HasInput" , GameInputManager.MainInstance.Movement != Vector2.zero);
            if (Anim.GetBool("HasInput"))
            {
                Anim.SetBool("Run" , GameInputManager.MainInstance.Run);
                Anim.SetFloat("Movement" , (Anim.GetBool("Run")?2f : GameInputManager.MainInstance.Movement.sqrMagnitude) , 0.25f , Time.deltaTime);
            }
            else
            {
                Anim.SetFloat("Movement" , 0f , 0.25f , Time.deltaTime);
                if (Anim.GetFloat("Movement") > 0.2f)
                {
                    Anim.SetBool("Run" , false);
                }
            }
        }
    }
}
