using System;
using Manager;
using UnityEngine;

namespace Base
{
    //受伤 处决 格挡 生命值
    public class CharacterHealthBase : MonoBehaviour
    {

        protected Transform CurrentAttacker;

        protected Animator Anim;

        protected virtual void OnEnable()
        {
            GameEventManager.MainInstance.AddEventListening<float , string , string , Transform , Transform>("TakeDamage",
                OnCharacterHitEventHandler);
        }

        private void Awake()
        {
            Anim = GetComponent<Animator>();
        }

        protected virtual void OnDisable()
        {
            GameEventManager.MainInstance.RemoveEvent<float , string , string , Transform , Transform>("TakeDamage",
                OnCharacterHitEventHandler);
        }

        /// <summary>
        /// 受伤行为
        /// </summary>
        /// <param name="damage">受击伤害</param>
        /// <param name="hitName">受击动画</param>
        /// <param name="parryName">格挡动画</param>
        protected virtual void CharacterHitAction(float damage , string hitName , string parryName)
        {
            
        }

        protected void TakeDamage(float damage)
        {
            
        }
        
        /// <summary>
        /// 设置当前攻击者
        /// </summary>
        /// <param name="attacker"></param>
        private void SetAttacker(Transform attacker)
        {
            if(CurrentAttacker == null || CurrentAttacker != attacker)
                CurrentAttacker = attacker;
        }
        
        private void OnCharacterHitEventHandler(float damage, string hitName, string parryName, Transform attack,
            Transform self)
        {
            if (self != transform) return;
            
            SetAttacker(attack);
            CharacterHitAction(damage , hitName , parryName);
            TakeDamage(damage);
        }
    }
}