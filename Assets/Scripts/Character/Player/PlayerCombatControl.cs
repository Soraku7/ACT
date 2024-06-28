using Base;
using GGG.Tool;
using Input;
using Manager;
using ScriptObjects;
using Unilts.Tools.DevelopmentTool;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Character.Player
{
    public class PlayerCombatControl : CharacterCombatBase
    {
        private Transform _camera;
        
        [SerializeField, Header("重攻击")] private CharacterCombo _HeavyCombo;
        [SerializeField, Header("暗杀攻击")] private CharacterCombo _assassinCombo;

        [SerializeField, Header("攻击检测")] private float _detectionRange;
        [SerializeField, Header("攻击检测")] private float _detectionDistance;
        [SerializeField] private LayerMask _enemyLayer;

        private Collider[] _units;
        private Vector3 _detectionDirection;

        protected override void Awake()
        {
            base.Awake();
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
            CanAttackInput = true;
            CanFinish = false;
            CurrentCombo = baseCombo;
            _units = new Collider[] { };
        }

        protected override void Update()
        {
            base.Update();
            //UpdateDetectDirection();
            
            ClearEnemy();
            
            CharacterBaseAttackInput();
            
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
            if (!CanAttackInput) return false;
            if (Anim.AnimationAtTag("Hit")) return false;
            if (Anim.AnimationAtTag("Parry")) return false;
            if (Anim.AnimationAtTag("Finishing")) return false;
            if (Anim.AnimationAtTag("Dash")) return false;
            return true;
        }

        protected override void CharacterBaseAttackInput()
        {
            base.CharacterBaseAttackInput();
            if (!CanBaseAttackInput()) return;
            if (GameInputManager.MainInstance.LAttack)
            {
                //判断当前组合技是否为空或者基础组合技
                if (CurrentCombo == null || CurrentCombo != baseCombo)
                {
                    ChangeComboData(baseCombo);
                }

                ExecuteComboAction();
            }
            else if (GameInputManager.MainInstance.RAttack)
            {
                Debug.Log(CurrentComboCount);
                if (CurrentComboCount >= 3)
                {
                    ChangeComboData(_HeavyCombo);
                    switch (CurrentComboCount)
                    {
                        case 3:
                            CurrentComboIndex = 0;
                            break;
                        case 4:
                            CurrentComboIndex = 1;
                            break;
                        case 5:
                            CurrentComboIndex = 2;
                            break;
                    }
                }
                else
                {
                    return;
                }

                ExecuteComboAction();
                CurrentComboCount = 0;
            }
        }
        

        /// <summary>
        /// 冲刺攻击
        /// </summary>
        private void DashAttack()
        {

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
                currentEnemy = hit.collider.transform;
            }

        }

        private void GetNearUnit()
        {
            if (currentEnemy != null) return;
            
            _units = Physics.OverlapSphere(transform.position + (transform.up * 0.7f), _detectionRange, _enemyLayer,
                QueryTriggerInteraction.Ignore);
        }

        private void GetOneEnemyUnit()
        {
            if (_units.Length == 0) return;
            if (currentEnemy != null && DevelopmentToos.DistanceForTarget(currentEnemy , transform) > 3f) return;
            if (!Anim.AnimationAtTag("Attack")) return;
            if (Anim.GetFloat(AnimationID.MovementID) > .7f) return;

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

            currentEnemy = tempEnemy != null ? tempEnemy : currentEnemy;
        }

        private void ClearEnemy()
        {
            if (currentEnemy == null) return;
            // if (Anim.GetFloat(AnimationID.MovementID) > .7f)
            // {
            //     currentEnemy = null;
            // }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position + (transform.forward * _detectionDistance),
                _detectionRange);
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
        /// 是否进行处决
        /// </summary>
        /// <returns></returns>
        private bool CanSpecialAttack()
        {
            if (Anim.AnimationAtTag("Finish")) return false;
            if (Anim.AnimationAtTag("Assassinate")) return false;
            if (currentEnemy == null) return false;
            if (CurrentComboCount < 2) return false;
            if (!CanFinish) return false;
            
            return true;
        }

        private void CharacterFinishAttackInput()
        {
            if (!CanSpecialAttack()) return;
            if (GameInputManager.MainInstance.Grab)
            {
                CurrentComboIndex = Random.Range(0, finishCombo.TryComboMaxCount());
                Anim.Play(finishCombo.TryGetOneComboAction(CurrentComboIndex));

                GameEventManager.MainInstance.CallEvent("Execute", finishCombo.TryGetOneHitName(CurrentComboIndex, 0),
                    transform, currentEnemy);
                ResetComboInfo();
                CurrentComboCount = 0;
                CanFinish = false;
            }
        }

        protected override void MatchPosition()
        {
            base.MatchPosition();
            
            if (Anim.AnimationAtTag("Finish"))
            {
                Debug.Log("位置匹配");
                transform.rotation = Quaternion.LookRotation(-currentEnemy.forward);
                RunningMatch(finishCombo , FinishComboIndex);
            }
            else if (Anim.AnimationAtTag("Assassinate"))
            {
                Debug.Log("位置匹配");
                transform.rotation = Quaternion.LookRotation(currentEnemy.forward);
                RunningMatch(_assassinCombo , FinishComboIndex);
            }

        }

        /// <summary>
        /// 是否允许暗杀
        /// </summary>
        /// <returns></returns>
        private bool CanAssassination()
        {
            if (currentEnemy == null) return false;
            if (DevelopmentToos.DistanceForTarget(currentEnemy, transform) > 1.7f) return false;
            if (Vector3.Angle(transform.forward, currentEnemy.forward) > 20f) return false;
            if (Anim.AnimationAtTag("Assassinate")) return false;
            if (Anim.AnimationAtTag("Finish")) return false;
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
                FinishComboIndex = Random.Range(0, _assassinCombo.TryComboMaxCount());
                Anim.Play(_assassinCombo.TryGetOneComboAction(FinishComboIndex), 0, 0);
                GameEventManager.MainInstance.CallEvent("Assassinate",
                    _assassinCombo.TryGetOneHitName(FinishComboIndex, 0), transform, currentEnemy);
                ResetComboInfo();
            }
        }

        private void EnableFinishEvenHandler(bool apply)
        {
            if (CanFinish) return;
            CanFinish = apply;
            
            Debug.Log("可以处决");
        }
    }
}