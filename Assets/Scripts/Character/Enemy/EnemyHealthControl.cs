using Base;
using GGG.Tool;
using Manager;
using UnityEngine;

namespace Character.Enemy
{
    public class EnemyHealthControl : CharacterHealthBase
    {
        protected override void Awake()
        {
            base.Awake();
            _characterHealthInfo = ScriptableObject.Instantiate(healthInfo);
            
            EnemyManager.MainInstance.AddEnemyUnit(gameObject);
        }

        
        protected override void CharacterHitAction(float damage , string hitName, string parryName)
        {
            // //damage > 30 为破防攻击
            if (damage < 30f && _characterHealthInfo.StrengthFull)
            {
                //不破防 进行闪避格挡计算
                if (!Anim.AnimationAtTag("Attack"))
                {
                    Anim.Play(parryName , 0 , 0);
                
                    GamePoolManager.MainInstance.TryGetPoolItem("BlockSound" , transform.position , Quaternion.identity);
                    _characterHealthInfo.DamageToStrength(damage);
                }
            
            }
            else
            {
                
                Anim.Play(hitName , 0 , 0);
                
                GamePoolManager.MainInstance.TryGetPoolItem("HitSound" , transform.position , Quaternion.identity);
                TakeDamage(damage);
            }
            
            //当前架势条为0
            //或者生命值小于20时  触发处决状态
            if (!_characterHealthInfo.StrengthFull)
            {
                GameEventManager.MainInstance.CallEvent("EnableFinish" , true);
            }
            
            if (_characterHealthInfo.CurrentHP < 20f)
            {
                //无论如何生命值低于20就可以处决
                Debug.Log("生命值低于20 触发处决");
                GameEventManager.MainInstance.CallEvent("EnableFinish" , true);
            }
            
            //移除敌人且暂停动画
            //TODO::添加角色消融特效
            if (_characterHealthInfo.IsDie)
            {
                EnemyManager.MainInstance.RemoveEnemyUnity(gameObject);
                if (TryGetComponent(out Animator enemyAnim))
                {
                    enemyAnim.speed = 0f;
                }
            }
        }
    }
}