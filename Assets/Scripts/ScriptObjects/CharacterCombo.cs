using System.Collections.Generic;
using UnityEngine;

namespace ScriptObjects
{
    [CreateAssetMenu(fileName = "Combo", menuName = "Create/Character/Combo", order = 0)]
    public class CharacterCombo : ScriptableObject
    {
        [SerializeField] private List<CharacterComboData> allComboData;

        public string TryGetOneComboAction(int index)
        {
            if (allComboData.Count == 0) return null;
            return allComboData[index].ComboName;
        }
        
        /// <summary>
        /// 获取被攻击的招式信息
        /// </summary>
        /// <param name="index"></param>
        /// <param name="hitIndex"></param>
        /// <returns></returns>
        public string TryGetOneHitName(int index , int hitIndex)
        {
            if (allComboData.Count == 0) return null;
            if (allComboData[index].GetHitNameMaxCount() == 0) return null;
            return allComboData[index].ComboHitName[hitIndex];
        }

        public float TryGetComboDamage(int index)
        {
            return allComboData.Count == 0 ? 0f : allComboData[index].Damage;
        }

        public float TryGetColdTime(int index)
        {
            return allComboData.Count == 0 ? 0f : allComboData[index].ColdTime;
        }
        
        public float TryGetComboPosition(int index)
        {
            return allComboData.Count == 0 ? 0f : allComboData[index].ComboPositionOffset;
        }

        /// <summary>
        /// 攻击段数
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int TryGetHitMaxCount(int index) => allComboData[index].GetHitNameMaxCount();
        /// <summary>
        /// 连招数量
        /// </summary>
        /// <returns></returns>
        public int TryComboMaxCount() => allComboData.Count;
    }
}