using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboData" , menuName = "Create/Character/ComboData" , order = 0)]
public class CharacterComboData : ScriptableObject
{
    [SerializeField] private string comboName;
    [SerializeField] private float damage;
    [SerializeField] private string[] comboHitName;
    [SerializeField] private string[] comboParryName;
    [SerializeField] private float coldTime;
    [SerializeField] private float comboPositionOffset;

    public string ComboName => comboName;
    public float Damage => damage;
    public string[] ComboHitName => comboHitName;
    public string[] ComboParryName => comboParryName;
    public float ColdTime => coldTime;
    public float ComboPositionOffset => comboPositionOffset;

    public int GetHitNameMaxCount() => comboHitName.Length;
}
