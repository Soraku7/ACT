using ScriptObjects.Health.CharacterHealthBase;
using UnityEngine;

namespace ScriptObjects.Health.CharacterHealthInfo
{
    [CreateAssetMenu(fileName = "CharacterHealthInfo", menuName = "Create/Character/HealthData/CharacterHealthInfo", order = 0)]
    public class CharacterHealthInfo : ScriptableObject
    {
        private float _currentHP;
        private float _currentStrength;
        private float _maxHP;
        private float _maxStrength;
        private bool _strengthFull;
        private bool _isDie => (_currentHP <= 0);
        
        public float CurrentHP => _currentHP;
        public float CurrentStrength => _currentStrength;
        public float MaxHP => _maxHP;
        public float MaxStrength => _maxStrength;
        public bool StrengthFull => _strengthFull;
        public bool IsDie => _isDie;

        [SerializeField] private CharacterHealthBaseData _characterHealthBase;

        public void InitCharacterHealthInfo()
        {
            _maxHP = _characterHealthBase.MaxHP;
            _maxStrength = _characterHealthBase.MaxStrength;
            _currentHP = _characterHealthBase.MaxHP;
            _currentStrength = _characterHealthBase.MaxStrength;
            _strengthFull = true;
        }
        
        public void Damage(float damage)
        {
            if (StrengthFull)
            {
                Debug.Log("体力值充足");
                _currentStrength = Clamp(_currentStrength ,damage, 0f, _maxStrength);

                if (_currentStrength <= 0) _strengthFull = false;
            }
            _currentHP = Clamp(_currentHP , damage, 0f, _maxStrength);
            
            Debug.Log("当前敌人血量" + _currentHP);
        }
        
        public void DamageToStrength(float damage)
        {
            if (StrengthFull)
            {
                Debug.Log("架势充足");
                _currentStrength = Clamp(_currentStrength, damage, 0f, _maxStrength);
            }
            else _currentStrength = Clamp(_currentStrength , damage, 0f, _maxStrength);
            if (_currentStrength <= 0) _strengthFull = false;
            Debug.Log("当前敌人血量" + _currentStrength);

        }

        public void AddHP(float hp)
        {
            _currentHP = Clamp(_currentHP, hp, 0, _maxHP, true);
        }

        public void AddStrength(float strength)
        {
            _currentStrength = Clamp(_currentStrength, strength, 0, _maxStrength, true);

            if (_currentStrength == _maxStrength) _strengthFull = true;
        }

        private float Clamp(float value , float offsetValue , float min , float max , bool add = false)
        {
            return Mathf.Clamp((add) ? value + offsetValue : value - offsetValue, min, max);
        }
    }
}