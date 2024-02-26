using System;
using GGG.Tool;
using Manager;
using ScriptObjects.Health.CharacterHealthInfo;
using UnityEngine;

namespace Base
{
    //受伤 处决 格挡 生命值
    public class CharacterHealthBase : MonoBehaviour
    {

        protected Transform CurrentAttacker;

        protected Animator Anim;

        [SerializeField] protected CharacterHealthInfo _characterHealthInfo;

        protected virtual void OnEnable()
        {
            GameEventManager.MainInstance.AddEventListening<float , string , string , Transform , Transform>("TakeDamage",
                OnCharacterHitEventHandler);
            GameEventManager.MainInstance.AddEventListening<string, Transform, Transform>("Execute",
                OnCharacterFinishEventHandler);
            GameEventManager.MainInstance.AddEventListening<float , Transform>("CreateDamage", TriggerDamageEventHandler);
            GameEventManager.MainInstance.AddEventListening<string, Transform, Transform>("Assassinate",
                OnCharacterAssassinationEventHandler);
        }

        private void Awake()
        {
            Anim = GetComponent<Animator>();
        }
        
        private void Start()
        {
            _characterHealthInfo.InitCharacterHealthInfo();
        }

        private void Update()
        {
            OnHitCharacterLookAttacker();
        }

        protected virtual void OnDisable()
        {
            GameEventManager.MainInstance.RemoveEvent<float , string , string , Transform , Transform>("TakeDamage",
                OnCharacterHitEventHandler);
            GameEventManager.MainInstance.RemoveEvent<string, Transform, Transform>("Execute",
                OnCharacterFinishEventHandler);
            GameEventManager.MainInstance.RemoveEvent<float , Transform>("CreateDamage", TriggerDamageEventHandler);
            GameEventManager.MainInstance.RemoveEvent<string, Transform, Transform>("Assassinate",
                OnCharacterAssassinationEventHandler);
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
            _characterHealthInfo.Damage(damage);
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

        private void OnHitCharacterLookAttacker()
        {
            if (CurrentAttacker == null) return;
            if (Anim.AnimationAtTag("Hit") || Anim.AnimationAtTag("Parry") &&
                Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
            {
                transform.Look(CurrentAttacker.position , 50f);
            }
                
        }
        
        private void OnCharacterHitEventHandler(float damage, string hitName, string parryName, Transform attack,
            Transform self)
        {
            if (self != transform) return;
            
            SetAttacker(attack);
            Anim.Play(parryName);
            _characterHealthInfo.DamageToStrength(damage);
        }

        private void OnCharacterFinishEventHandler(string hitName, Transform attacker, Transform self)
        {
            if (self != transform) return;
            SetAttacker(attacker);
            Anim.Play(hitName);
        }
        
        private void OnCharacterAssassinationEventHandler(string hitName, Transform attacker, Transform self)
        {
            if (self != transform) return;
            SetAttacker(attacker);
            Anim.Play(hitName);
        }

        private void TriggerDamageEventHandler(float damage , Transform self)
        {
            if (self != transform) return;
            
            TakeDamage(damage);
            GamePoolManager.MainInstance.TryGetPoolItem("HitSound" , transform.position , Quaternion.identity);
        }
    }
}