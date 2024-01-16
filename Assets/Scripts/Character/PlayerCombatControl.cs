using System;
using GGG.Tool;
using Input;
using Manager;
using ScriptObjects;
using UnityEngine;

namespace Character
{
    public class PlayerCombatControl : MonoBehaviour
    {
        private Animator _animator;
        
        [SerializeField, Header("角色组合技")] private CharacterCombo _baseCombo;
        private CharacterCombo _currentCombo;

        private int _currentComboIndex;
        private float _maxColdTime;
        private bool _canAttackInput;
        private int _hitIndex;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _canAttackInput = true;
            _currentCombo = _baseCombo;
        }

        private void Update()
        {
            CharacterBaseAttackInput();
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
                    _currentCombo = _baseCombo;
                    ResetComboInfo();
                } 
                ExecuteComboAction();
            }
        }

        private void ExecuteComboAction()
        {
            //更新当前Hit的索引值
            _hitIndex = 0;
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
    }
}