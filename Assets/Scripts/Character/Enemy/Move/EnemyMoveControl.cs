using Base;
using GGG.Tool;
using Manager;
using UnityEngine;

namespace Character.Enemy.Move
{
    public class EnemyMoveControl : CharacterMovementControlBase
    {
        private bool _applyMovement;


        protected override void Start()
        {
            base.Start();
            SetApplyMovement(true);
        }

        protected override void Update()
        {
            base.Update();
            LockTargetDirection();
        }
        
        //玩家在移动的时候观看AI
        private void LockTargetDirection()
        {
            if (Anim.AnimationAtTag("Motion"))
            {
                transform.Look(EnemyManager.MainInstance.GetMainPlayer().position , 500f );
            }
        }
        
        /// <summary>
        /// 设置动画移动参数
        /// </summary>
        /// <param name="horizontal"></param>
        /// <param name="vertical"></param>
        public void SetAnimatorMovementValue(float horizontal, float vertical)
        {
            if(_applyMovement)
            {
                Anim.SetFloat(AnimationID.LockID, 1f);
                Anim.SetFloat(AnimationID.HasInputID, horizontal, 0.2f,
                    Time.deltaTime);
                Anim.SetFloat(AnimationID.VerticalID, vertical, 0.2f,
                    Time.deltaTime);
            }
            else
            {
                Anim.SetFloat(AnimationID.LockID, 0f);
                Anim.SetFloat(AnimationID.HasInputID, horizontal, 0f,
                    Time.deltaTime);
                Anim.SetFloat(AnimationID.VerticalID, vertical, 0f,
                    Time.deltaTime);
            }
        }

        public void SetApplyMovement(bool apply)
        {
            _applyMovement = apply;
        }
    }
}