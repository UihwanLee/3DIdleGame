using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ConditionInfo
/// 게임 내 모든 Condition Value 관리함
/// </summary>
[CreateAssetMenu(fileName = "ConditionData", menuName = "Data/Condition")]
public class ConditionInfoSO : ScriptableObject
{
    [field: Header("Exp")]
    [field: SerializeField] public float BaseMaxExp { get; private set; }
    [field: SerializeField][field: Range(1f, 2f)] public float ExpIncreaseScaling { get; private set; }

    [field: Header("Hp")]
    [field: SerializeField] public float BaseMaxHp { get; private set; }
    [field: SerializeField][field: Range(1f, 2f)] public float HpIncreaseScaling { get; private set; }

    [field: Header("Atk")]
    [field: SerializeField] public float BaseAtk;
    [field: SerializeField][field: Range(1f, 2f)] public float AtkIncreasScaling { get; private set; }
}
