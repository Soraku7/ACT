using UnityEngine;

namespace Character.Enemy.Combat
{
    public class EnemyCombatControl : MonoBehaviour
    {
        private bool _attackCommand;


        /// <summary>
        /// 获取AI的攻击指令
        /// </summary>
        /// <returns></returns>
        public bool GetAttackCommand() => _attackCommand;
    }
}