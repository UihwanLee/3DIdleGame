using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerBaseData
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; private set; }
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; }

    [field: Header("IdleData")]

    [field: Header("WalkData")]
    [field: SerializeField][field: Range(0f, 2f)] public float WalkSpeedModifier { get; private set; }

    [field: Header("RunData")]
    [field: SerializeField][field: Range(0f, 2f)] public float RunSpeedModifier { get; private set; }
}

[Serializable]
public class PlayerAttackData
{
    [field:SerializeField] public List<AttackInfoData> AttackInfoDatas { get; private set; }
    public int GetAttackInfoCount() { return AttackInfoDatas.Count; }
    public AttackInfoData GetAttackInfoData(int index) { return  AttackInfoDatas[index]; }

    [field: SerializeField][field: Range(5f, 20f)] public float AttackDamage { get; private set; }
    [field: SerializeField][field: Range(0f, 5f)] public float AttackCoolTime { get; private set; }
}

[Serializable]
public class AttackInfoData
{
    [field:SerializeField] public string AttackName { get; private set; }
    [field: SerializeField] public int ComboStateIndex { get; private set; }
    [field: SerializeField][field:Range(0f, 1f)] public float ComboTransitionTime { get; private set; }
    [field: SerializeField][field:Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
    [field: SerializeField][field:Range(-10f, 10f)] public float Force { get; private set; }
    [field: SerializeField] public int Damage;
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
}



[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field:SerializeField] public PlayerBaseData BaseData { get; private set; }
    [field:SerializeField] public PlayerAttackData AttackData { get; private set; }
}

