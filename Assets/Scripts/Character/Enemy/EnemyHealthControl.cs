using Base;
using Manager;
using UnityEngine;

namespace Character.Enemy
{
    public class EnemyHealthControl : CharacterHealthBase
    {
        protected override void CharacterHitAction(float damage , string hitName, string parryName)
        {
            //damage > 30 为破防攻击
            if (damage < 30f)
            {
                //不破防 进行闪避格挡计算
            }
            else
            {
                Anim.Play(hitName , 0 , 0);
                
                GamePoolManager.MainInstance.TryGetPoolItem("HitSound" , transform.position , Quaternion.identity);
            }
        }
    }
}