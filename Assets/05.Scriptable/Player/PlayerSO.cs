using System;
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
    [field: SerializeField][field: Range(5f, 20f)] public float AttackDamage { get; private set; }
}



[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field:SerializeField] public PlayerBaseData BaseData { get; private set; }
    [field:SerializeField] public PlayerAttackData AttackData { get; private set; }
}

