using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DropItemTable
{
    [field: SerializeField] public GameObject DropItem { get; private set; }
    [field: SerializeField] public float DropPercentage { get; private set; }
}

[Serializable]
public class RewardInfo
{
    [field: SerializeField] public float GainExp { get; private set; }
    [field: SerializeField] public float GainGold { get; private set; }
    [field: SerializeField] public List<DropItemTable> DropItemTables { get; private set; }
}

[CreateAssetMenu(fileName = "Monster", menuName = "Characters/Monster")]
public class MonsterSO : ScriptableObject
{
    [field: SerializeField] public float ChaseRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field:SerializeField] public float AttackCoolTime { get; private set; }
    [field: SerializeField] public int Damage;
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
    [field: SerializeField] public RewardInfo RewardInfo { get; private set; }
}
