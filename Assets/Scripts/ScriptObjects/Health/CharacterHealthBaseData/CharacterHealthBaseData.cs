using UnityEngine;

namespace ScriptObjects.Health.CharacterHealthBase
{
    [CreateAssetMenu(fileName = "ComboData", menuName = "Create/Character/HealthData/BaseData", order = 0)]
    public class CharacterHealthBaseData : ScriptableObject
    {
        [SerializeField] private float _maxHP;
        [SerializeField] private float _maxStrength;

        /// <summary>
        /// 初始最大生命值
        /// </summary>
        public float MaxHP => _maxHP;
        /// <summary>
        /// 初始最大体力值
        /// </summary>
        public float MaxStrength => _maxStrength;

    }
}