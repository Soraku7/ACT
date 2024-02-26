using Base;
using GGG.Tool;
using Manager;
using UnityEngine;

namespace Character.Enemy
{
    public class EnemyHealthControl : CharacterHealthBase
    {
        protected override void CharacterHitAction(float damage , string hitName, string parryName)
        {
            // //damage > 30 为破防攻击
            if (damage < 30f && _characterHealthInfo.StrengthFull)
            {
                //不破防 进行闪避格挡计算
                if (!Anim.AnimationAtTag("Attack"))
                {
                    Anim.Play(hitName , 0 , 0);
                
                    GamePoolManager.MainInstance.TryGetPoolItem("BlockSound" , transform.position , Quaternion.identity);
                    _characterHealthInfo.DamageToStrength(damage);
                    if (!_characterHealthInfo.StrengthFull)
                    {
                        GameEventManager.MainInstance.CallEvent("EnableFinish" , true);
                    }
                }
            
            }
            else
            {
                if (_characterHealthInfo.CurrentHP < 20f)
                {
                    GameEventManager.MainInstance.CallEvent("EnableFinish" , true);
                }
                
                Anim.Play(hitName , 0 , 0);
                
                GamePoolManager.MainInstance.TryGetPoolItem("HitSound" , transform.position , Quaternion.identity);
            }
            // Debug.Log("造成伤害");
            // Anim.Play(hitName , 0 , 0);
            //     
            // GamePoolManager.MainInstance.TryGetPoolItem("HitSound" , transform.position , Quaternion.identity);
        }
    }
}