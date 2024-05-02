using System;
using System.Runtime.InteropServices.WindowsRuntime;
using GGG.Tool;
using Input;
using Manager;
using ScriptObjects;
using Unilts.Tools.DevelopmentTool;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Character
{
    public class PlayerCombatControl : MonoBehaviour
    {
        private Animator _animator;
        private Transform _camera;

        [SerializeField, Header("角色组合技")] private CharacterCombo _baseCombo;
        [SerializeField, Header("重攻击")] private CharacterCombo _HeavyCombo;
        [SerializeField, Header("处决攻击")] private CharacterCombo _finishCombo;
        [SerializeField, Header("暗杀攻击")] private CharacterCombo _assassinCombo;
        private CharacterCombo _currentCombo;

        private int _currentComboIndex;
        private int _currentComboCount;
        private float _maxColdTime;
        private bool _canAttackInput;
        private int _hitIndex;

        private int _finishComboIndex;
        private bool _canFinish;    

        [SerializeField, Header("攻击检测")] private float _detectionRange;
        [SerializeField, Header("攻击检测")] private float _detectionDistance;
        [SerializeField] private LayerMask _enemyLayer;

        private Collider[] _units;
        private Vector3 _detectionDirection;
        public Transform _currentEnemy;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            if (Camera.main != null) _camera = Camera.main.transform;
        }

        private void OnEnable()
        {
            GameEventManager.MainInstance.AddEventListening<bool>("EnableFinish" , EnableFinishEvenHandler);
        }

        private void OnDisable()
        {
            GameEventManager.MainInstance.RemoveEvent<bool>("EnableFinish" , EnableFinishEvenHandler);
        }

        private void Start()
        {
            _canAttackInput = true;
            _canFinish = false;
            _currentCombo = _baseCombo;
            _units = new Collider[] { };
        }

        private void Update()
        {
            //UpdateDetectDirection();
            
            ClearEnemy();
            
            CharacterBaseAttackInput();
            OnEndAttack();
            
            LookTargetOnAttack();
            CharacterFinishAttackInput();
            MatchPosition();
            CharacterAssassinationInput();
            
            GetNearUnit();
        }

        private void FixedUpdate()
        {
            //DetectionTarget();
            GetOneEnemyUnit();
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
                Debug.Log(_currentComboCount);
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
            TimeManager.MainInstance.TryGetOneTimer(_maxColdTime, UpdateComboInfo);
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
            _hitIndex = 0;
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
            UpdateHitIndex();
            GamePoolManager.MainInstance.TryGetPoolItem("AtkSound", transform.position, Quaternion.identity);
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

        private void GetNearUnit()
        {
            if (_currentEnemy != null) return;
            
            _units = Physics.OverlapSphere(transform.position + (transform.up * 0.7f), _detectionRange, _enemyLayer,
                QueryTriggerInteraction.Ignore);
        }

        private void GetOneEnemyUnit()
        {
            if (_units.Length == 0) return;
            if (_currentEnemy != null && DevelopmentToos.DistanceForTarget(_currentEnemy , transform) > 3f) return;
            if (!_animator.AnimationAtTag("Attack")) return;
            if (_animator.GetFloat(AnimationID.MovementID) > .7f) return;

            Transform tempEnemy = null;
            var distance = Mathf.Infinity;
            foreach (var e in _units)
            {
                var dis = DevelopmentToos.DistanceForTarget(e.transform, transform);
                if (dis < distance)
                {
                    tempEnemy = e.transform;
                    distance = dis;
                }
            }

            _currentEnemy = tempEnemy != null ? tempEnemy : _currentEnemy;
        }

        private void ClearEnemy()
        {
            if (_currentEnemy == null) return;
            // if (_animator.GetFloat(AnimationID.MovementID) > .7f)
            // {
            //     _currentEnemy = null;
            // }
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
                GameEventManager.MainInstance.CallEvent("TakeDamage",
                    _currentCombo.TryGetComboDamage(_currentComboIndex),
                    _currentCombo.TryGetOneHitName(_currentComboIndex, _hitIndex),
                    _currentCombo.TryGetOneParryName(_currentComboIndex, _hitIndex), transform, _currentEnemy);
            }
            else
            {
                //进行处决动画
                GameEventManager.MainInstance.CallEvent("CreateDamage",
                    _finishCombo.TryGetComboDamage(_currentComboIndex), _currentEnemy);
            }
        }

        /// <summary>
        /// 更新检测方向
        /// </summary>
        private void UpdateDetectDirection()
        {
            _detectionDirection = (_camera.forward * GameInputManager.MainInstance.Movement.y) +
                                  (_camera.right * GameInputManager.MainInstance.Movement.x);
            _detectionDirection.Set(_detectionDirection.x, 0f, _detectionDirection.z);
            _detectionDirection = _detectionDirection.normalized;
        }

        /// <summary>
        /// 角色朝向攻击目标
        /// </summary>
        private void LookTargetOnAttack()
        {
            if (_currentEnemy == null) return;
            if (DevelopmentToos.DistanceForTarget(_currentEnemy, transform) > 5f) return;
            if (_animator.AnimationAtTag("Attack") && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
            {
                //动画未执行到一半
                // transform.rotation = Quaternion.LookRotation(_currentEnemy.position);
                transform.Look(_currentEnemy.position, 50f);
            }
        }

        private void UpdateHitIndex()
        {
            _hitIndex++;
            Debug.Log(_currentComboIndex);
            if (_hitIndex == _currentCombo.TryGetHitMaxCount(_currentComboIndex))
                _hitIndex = 0;
        }


        /// <summary>
        /// 是否进行处决
        /// </summary>
        /// <returns></returns>
        private bool CanSpecialAttack()
        {
            if (_animator.AnimationAtTag("Finish")) return false;
            if (_animator.AnimationAtTag("Assassinate")) return false;
            if (_currentEnemy == null) return false;
            if (_currentComboCount < 2) return false;
            if (!_canFinish) return false;
            
            return true;
        }

        private void CharacterFinishAttackInput()
        {
            if (!CanSpecialAttack()) return;
            if (GameInputManager.MainInstance.Grab)
            {
                _currentComboIndex = Random.Range(0, _finishCombo.TryComboMaxCount());
                _animator.Play(_finishCombo.TryGetOneComboAction(_currentComboIndex));

                GameEventManager.MainInstance.CallEvent("Execute", _finishCombo.TryGetOneHitName(_currentComboIndex, 0),
                    transform, _currentEnemy);
                ResetComboInfo();
                _currentComboCount = 0;
                _canFinish = false;
            }
        }

        private void MatchPosition()
        {
            if (_currentEnemy == null) return;
            if (!_animator) return;

            if (_animator.AnimationAtTag("Finish"))
            {
                Debug.Log("位置匹配");
                transform.rotation = Quaternion.LookRotation(-_currentEnemy.forward);
                RunningMatch(_finishCombo , _finishComboIndex);
            }
            else if (_animator.AnimationAtTag("Assassinate"))
            {
                Debug.Log("位置匹配");
                transform.rotation = Quaternion.LookRotation(_currentEnemy.forward);
                RunningMatch(_assassinCombo , _finishComboIndex);
            }
        }

        private void RunningMatch(CharacterCombo combo, int index ,float startTime = 0f, float endTime = 0.01f)
        {
            if (!_animator.isMatchingTarget && _animator.IsInTransition(0))
            {
                _animator.MatchTarget(
                    _currentEnemy.position +
                    (-transform.forward * combo.TryGetComboPosition(index)),
                    Quaternion.identity, AvatarTarget.Body, new MatchTargetWeightMask(Vector3.one, 0f), startTime,
                    endTime);
            }

        }

        /// <summary>
        /// 是否允许暗杀
        /// </summary>
        /// <returns></returns>
        private bool CanAssassination()
        {
            if (_currentEnemy == null) return false;
            if (DevelopmentToos.DistanceForTarget(_currentEnemy, transform) > 1.7f) return false;
            if (Vector3.Angle(transform.forward, _currentEnemy.forward) > 20f) return false;
            if (_animator.AnimationAtTag("Assassinate")) return false;
            if (_animator.AnimationAtTag("Finish")) return false;
            return true;
        }

        /// <summary>
        /// 触发暗杀
        /// </summary>
        private void CharacterAssassinationInput()
        {
            if (!CanAssassination()) return;
            
            if (GameInputManager.MainInstance.Takeout)
            {
                _finishComboIndex = Random.Range(0, _assassinCombo.TryComboMaxCount());
                _animator.Play(_assassinCombo.TryGetOneComboAction(_finishComboIndex), 0, 0);
                GameEventManager.MainInstance.CallEvent("Assassinate",
                    _assassinCombo.TryGetOneHitName(_finishComboIndex, 0), transform, _currentEnemy);
                ResetComboInfo();
            }
        }

        private void EnableFinishEvenHandler(bool apply)
        {
            if (_canFinish) return;
            _canFinish = apply;
            
            Debug.Log("可以处决");
        }
    }
}