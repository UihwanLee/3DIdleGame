using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Monster", menuName = "Characters/Monster")]
public class MonsterSO : ScriptableObject
{
    [field: SerializeField] public float ChaseRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field:SerializeField] public float AttackCoolTime { get; private set; }
    [field: SerializeField] public int Damage;
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
}
