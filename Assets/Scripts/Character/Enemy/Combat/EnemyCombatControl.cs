using Base;
using GGG.Tool;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character.Enemy.Combat
{
    public class EnemyCombatControl : CharacterCombatBase
    {
        [SerializeField] private bool attackCommand;

        private void Start()
        {
            CanAttackInput = true;
        }

        public void AIBaseAttackInput()
        {
            currentEnemy = GameObject.FindWithTag("Player").transform;
            if (!CanAttackInput) return;
            ChangeComboData(baseCombo);
            ExecuteComboAction();
        }

        protected override void UpdateComboInfo()
        {
            CurrentComboIndex++;
            if (CurrentComboIndex == CurrentCombo.TryComboMaxCount())
            {
                CurrentComboIndex = 0;
                //敌人攻击执行完成
                ResetAttackCommand();
            }

            MaxColdTime = 0;
            CanAttackInput = true;
        }
        
        /// <summary>
        /// 判断当前AI是否能够接受攻击指令
        /// </summary>
        /// <returns></returns>
        private bool CheckAIState()
        {
            if (Anim.AnimationAtTag("Hit")) return false;
            if (Anim.AnimationAtTag("Parry")) return false;
            if (Anim.AnimationAtTag("FinishHit")) return false;

            return true;
        }

        private void ResetAttackCommand()
        {
            attackCommand = false; 
        }

        /// <summary>
        /// 获取AI的攻击指令
        /// </summary>
        /// <returns></returns>
        public bool GetAttackCommand() => attackCommand;

        public void SetAttackCommand(bool command)
        {
            if (!CheckAIState())
            {
                ResetAttackCommand();
                return;
            }

            attackCommand = command;
        }
    }
}