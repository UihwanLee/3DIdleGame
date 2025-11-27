using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SkillType - Active / Passive가 아닌
// 공격 가능 / Buff로 나눔
public enum SkillTpye
{
    Active,
    Buff,
}

public enum BuffType
{
    SpeedUp,
    Heal,
}

[Serializable]
public class ActvieInfo
{
    [field: SerializeField][field: Range(1f, 3f)] public int DamageMultiplier { get; private set; }
    [field: SerializeField][field: Range(0f, 5f)] public float ExcuteDuration { get; private set; }
    [field: SerializeField][field: Range(0f, 5f)] public float TickInterval { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
    [field: SerializeField] public LayerMask TargetLayer {  get; private set; }
}

[Serializable]
public class BuffInfo
{
    [field: SerializeField] public float ValueMultiplier { get; private set; }
    [field: SerializeField] public BuffType Type { get; private set; }
    [field: SerializeField][field: Range(0f, 5f)] public float ExcuteDuration { get; private set; }
    [field: SerializeField][field: Range(0f, 5f)] public float TickInterval { get; private set; }
}

[CreateAssetMenu(fileName = "Skill", menuName = "Data/Skill")]
public class SkillSO : ScriptableObject
{
    [field: SerializeField] public string SkillName { get; private set; }
    [field: SerializeField] public Sprite SkillIcon { get; private set; }
    [field: SerializeField] public float SkillCoolTime { get; private set; }
    [field: SerializeField] public SkillTpye Type { get; private set; }

    [field: SerializeField] public ActvieInfo ActvieInfo { get; private set; }

    [field: SerializeField] public BuffInfo BuffInfo { get; private set; }
}
