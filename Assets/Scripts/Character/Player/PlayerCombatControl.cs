using System;
using GGG.Tool;
using Input;
using Manager;
using ScriptObjects;
using Unilts.Tools.DevelopmentTool;
using UnityEngine;

namespace Character
{
    public class PlayerCombatControl : MonoBehaviour
    {
        private Animator _animator;
        private Transform _camera;
        
        [SerializeField, Header("角色组合技")] private CharacterCombo _baseCombo;
        [SerializeField, Header("角色组合技")] private CharacterCombo _HeavyCombo;
        private CharacterCombo _currentCombo;

        private int _currentComboIndex;
        private int _currentComboCount;
        private float _maxColdTime;
        private bool _canAttackInput;
        private int _hitIndex;

        [SerializeField, Header("攻击检测")] private float _detectionRange;
        [SerializeField, Header("攻击检测")] private float _detectionDistance;
        private Vector3 _detectionDirection;
        private Transform _currentEnemy;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            if (Camera.main != null) _camera = Camera.main.transform;
        }

        private void Start()
        {
            _canAttackInput = true;
            _currentCombo = _baseCombo;
        }

        private void Update()
        {
            CharacterBaseAttackInput();
            OnEndAttack();
            UpdateDetectDirection();
            LookTargetOnAttack();

        }

        private void FixedUpdate()
        {
            DetectionTarget();
        }

        private bool CanBaseAttackInput()
        {
            //不允许攻击
            //角色正在受击
            //角色正在格挡
            //角色正在处决敌人
            if (!_canAttackInput) return false;
            if (_animator.AnimationAtTag("Hit")) return false;
            if (_animator.AnimationAtTag("Parry")) return false;
            if (_animator.AnimationAtTag("Finishing")) return false;
            if (_animator.AnimationAtTag("Dash")) return false;
            return true;
        }

        private void CharacterBaseAttackInput()
        {
            if (!CanBaseAttackInput()) return;
            if (GameInputManager.MainInstance.LAttack)
            {
                //判断当前组合技是否为空或者基础组合技
                if (_currentCombo == null || _currentCombo != _baseCombo)
                {
                   ChangeComboData(_baseCombo);
                } 
                ExecuteComboAction();
            }
            else if (GameInputManager.MainInstance.RAttack)
            {
                if (_currentComboCount >= 3)
                {
                    ChangeComboData(_HeavyCombo);
                    switch (_currentComboCount)
                    {
                        case 3:
                            _currentComboIndex = 0;
                            break;
                        case 4:
                            _currentComboIndex = 1;
                            break;
                        case 5:
                            _currentComboIndex = 2;
                            break;
                    }
                }
                else
                {
                    return;
                }
                
                ExecuteComboAction();
                _currentComboCount = 0;
            }
        }

        /// <summary>
        /// 退出连招
        /// </summary>
        private void ExecuteComboAction()
        {
            //更新当前Hit的索引值
            _hitIndex = 0;
            _currentComboCount += (_currentCombo == _baseCombo) ? 1 : 0;
            if (_currentComboIndex == _currentCombo.TryComboMaxCount())
            {
                //当前攻击动作为最后一个动作
                _currentComboIndex = 0;
            }

            _maxColdTime = _currentCombo.TryGetColdTime(_currentComboIndex);
            _animator.CrossFadeInFixedTime(_currentCombo.TryGetOneComboAction(_currentComboIndex), 0.1555f, 0, 0f);
            TimeManager.MainInstance.TryGetOneTimer(_maxColdTime , UpdateComboInfo);
            _canAttackInput = false;
        }

        /// <summary>
        /// 更新连招
        /// </summary>
        private void UpdateComboInfo()
        {
            _currentComboIndex++;
            _maxColdTime = 0f;
            _canAttackInput = true; 
        }
        
        /// <summary>
        /// 重置组合技
        /// </summary>
        private void ResetComboInfo()
        {
            _currentComboIndex = 0;
            _maxColdTime = 0;
        }

        /// <summary>
        /// 重置连招
        /// </summary>
        private void OnEndAttack()
        {
            if (_animator.AnimationAtTag("Motion") && _canAttackInput)
            {
                ResetComboInfo();
            }
        }

        /// <summary>
        /// 更换出招表格
        /// </summary>
        /// <param name="comboData"></param>
        private void ChangeComboData(CharacterCombo comboData)
        {
            if (_currentCombo != comboData)
            {
                _currentCombo = comboData;
                ResetComboInfo();
            }
        }

        /// <summary>
        /// 冲刺攻击
        /// </summary>
        private void DashAttack()
        {
            
        }

        /// <summary>
        /// 动画触发攻击事件
        /// </summary>
        private void Atk()
        {
            TriggerDamage();
            GamePoolManager.MainInstance.TryGetPoolItem("AtkSound" , transform.position , Quaternion.identity);
        }
        
        /// <summary>
        /// 检测目标
        /// </summary>
        private void DetectionTarget()
        {
            if (Physics.SphereCast(transform.position + (transform.up * 0.7f), _detectionRange, _detectionDirection,
                    out var hit, _detectionDistance, 1 << 9, QueryTriggerInteraction.Ignore))
            {
                //检测到敌人
                _currentEnemy = hit.collider.transform;
            }

        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + (transform.forward * _detectionDistance),
                _detectionRange);
        }

        /// <summary>
        /// 伤害触发
        /// </summary>
        private void TriggerDamage()
        {
            if (_currentEnemy == null) return;
            
            //判断敌人方向距离
            if (Vector3.Dot(transform.forward, DevelopmentToos.DirectionForTarget(transform, _currentEnemy)) <
                0.85f) return;
            if (DevelopmentToos.DistanceForTarget(transform, _currentEnemy) > 1.3f) return;
            
            if (_animator.AnimationAtTag("Attack"))
            {
                Debug.Log("AAA");
                GameEventManager.MainInstance.CallEvent("TakeDamage", _currentCombo.TryGetComboDamage(_currentComboIndex),
                    _currentCombo.TryGetOneHitName(_currentComboIndex, _hitIndex),
                    _currentCombo.TryGetOneParryName(_currentComboIndex, _hitIndex), transform, _currentEnemy);
            }
            else
            {
                //进行处决动画
            }
        }

        /// <summary>
        /// 更新检测方向
        /// </summary>
        private void UpdateDetectDirection()
        {
            _detectionDirection = (_camera.forward * GameInputManager.MainInstance.Movement.y) +
                                  (_camera.right * GameInputManager.MainInstance.Movement.x);
            _detectionDirection.Set(_detectionDirection.x , 0f , _detectionDirection.z);
            _detectionDirection = _detectionDirection.normalized;
        }

        private void LookTargetOnAttack()
        {
            if (_animator.AnimationAtTag("Attack") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f &&
                _currentEnemy != null)
            {
                //动画未执行到一半
                // transform.rotation = Quaternion.LookRotation(_currentEnemy.position);
            }
        }
    }
}