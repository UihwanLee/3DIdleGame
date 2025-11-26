using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuffInfo
{
    [field: SerializeField][field: Range(1f, 2f)] public float ValueMultiplier { get; private set; }
    [field: SerializeField] public BuffType Type { get; private set; }
    [field: SerializeField][field: Range(0f, 5f)] public float ExcuteDuration { get; private set; }
}

[CreateAssetMenu(fileName = "BuffSkill", menuName = "Skill/Buff")]
public class BuffSkillSO : SkillSO
{
    [field: SerializeField] public BuffInfo BuffInfo { get; private set; }

    public override void Excute(ISkillCaster caster)
    {
        caster.ApplyBuff(BuffInfo, BuffInfo.Type);
    }
}
