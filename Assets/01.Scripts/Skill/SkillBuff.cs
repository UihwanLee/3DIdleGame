using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBuff : Skill
{
    public SkillBuff(SkillSO data) : base(data)
    {
    }

    public override void Excute(ISkillCaster caster)
    {
        caster.ApplyBuff(_data.BuffInfo, _data.BuffInfo.Type);
    }
}
