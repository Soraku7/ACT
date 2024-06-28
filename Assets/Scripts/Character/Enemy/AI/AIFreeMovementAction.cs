using BehaviorDesigner.Runtime.Tasks;
using Character.Enemy.Combat;
using Character.Enemy.Move;
using Manager;
using Unilts.Tools.DevelopmentTool;
using UnityEngine;

namespace Character.Enemy.AI
{
    public class AIFreeMovementAction : Action
    {
        private EnemyMoveControl _enemyMoveControl;
        private EnemyCombatControl _enemyCombatControl;

        /// <summary>
        /// 动作索引
        /// </summary>
        private int _actionIndex;
        /// <summary>
        /// 上个动作的索引
        /// </summary>
        private int _lastActionIndex;
        private float _actionTimer;

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
                
                if (DistanceForTarget() > 8f)
                {
                    _enemyMoveControl.SetAnimatorMovementValue(0f , 1f);
                }
                else if(DistanceForTarget() < 8.0f && DistanceForTarget() > 3.0f)
                {
                    FreeMovement();
                    UpdateFreeAction();
                }
                else
                {
                    _enemyMoveControl.SetAnimatorMovementValue(-1f , -1f);
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

        private void FreeMovement()
        {
            switch (_actionIndex)
            {
                case 0:
                    //向左移动
                    _enemyMoveControl.SetAnimatorMovementValue(-1 , 0);
                    break;
                
                case 1:
                    //向右移动
                    _enemyMoveControl.SetAnimatorMovementValue(0 , 1);
                    break;
                
                case 2:
                    //向右移动
                    _enemyMoveControl.SetAnimatorMovementValue(0 , 0);
                    break;
                
                case 3:
                    //向右移动
                    _enemyMoveControl.SetAnimatorMovementValue(-1 , -1);
                    break;
                
                case 4:
                    //向右移动
                    _enemyMoveControl.SetAnimatorMovementValue(1 , 1);
                    break;
            }
        }

        private void UpdateFreeAction()
        {
            if (_actionTimer > 0)
            {
                _actionTimer -= Time.deltaTime;
                if (_actionTimer <= 0)
                {
                    UpdateActionIndex();
                }
            }
        }

        private void UpdateActionIndex()
        {
            _lastActionIndex = _actionIndex;
            _actionIndex = Random.Range(0, 5);
            _actionTimer = Random.Range(2, 3);
            if (_actionIndex == _lastActionIndex)
            {
                _actionIndex = Random.Range(0, 2);
            }

            if (_actionIndex == 3 || _actionIndex == 4)
            {
                
            }
        }
    }
}