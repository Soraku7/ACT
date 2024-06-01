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
            DrawDirection();
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
                Anim.SetBool(AnimationID.HasInputID , true);
                Anim.SetFloat(AnimationID.LockID, 2f);
                Anim.SetFloat(AnimationID.HorizontalID, horizontal, 0.2f,
                    Time.deltaTime);
                Anim.SetFloat(AnimationID.VerticalID, vertical, 0.2f,
                    Time.deltaTime);
            }
            else
            {
                Anim.SetBool(AnimationID.HasInputID , false);
                Anim.SetFloat(AnimationID.LockID, 0f);
                Anim.SetFloat(AnimationID.HorizontalID, 0, 0f,
                    Time.deltaTime);
                Anim.SetFloat(AnimationID.VerticalID, 0, 0f,
                    Time.deltaTime);
            }
        }

        public void SetApplyMovement(bool apply)
        {
            _applyMovement = apply;
        }

        public void DrawDirection()
        {
            Debug.DrawRay(transform.position + (transform.up * 0.7f),
                EnemyManager.MainInstance.GetMainPlayer().position - transform.position, Color.yellow);
        }
    }
}