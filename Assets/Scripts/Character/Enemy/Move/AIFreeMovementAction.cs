using BehaviorDesigner.Runtime.Tasks;
using Character.Enemy.Combat;
using Manager;
using Unilts.Tools.DevelopmentTool;
using UnityEngine;

namespace Character.Enemy.Move
{
    public class AIFreeMovementAction : Action
    {
        private EnemyMoveControl _enemyMoveControl;
        private EnemyCombatControl _enemyCombatControl;

        public override void OnAwake()
        {
            base.OnAwake();
            _enemyMoveControl = GetComponent<EnemyMoveControl>();
            _enemyCombatControl = GetComponent<EnemyCombatControl>();
        }

        public override TaskStatus OnUpdate()
        {

            if (!_enemyCombatControl.GetAttackCommand())
            {
                if (DistanceForTarget() > 0.3f)
                {
                    _enemyMoveControl.SetAnimatorMovementValue(0f , 1f);
                }
                else
                {
                    _enemyMoveControl.SetAnimatorMovementValue(0f , 0f);
                }
                
                return TaskStatus.Running;
            }
            else
            {
                
            }
            
            return TaskStatus.Success;
        }

        private float DistanceForTarget() => DevelopmentToos.DistanceForTarget(
            EnemyManager.MainInstance.GetMainPlayer(),
            _enemyMoveControl.transform);
    }
}