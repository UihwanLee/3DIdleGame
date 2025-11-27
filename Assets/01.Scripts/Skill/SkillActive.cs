using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActive : Skill
{
    public SkillActive(SkillSO data) : base(data)
    {
  
    }

    public override void Excute(ISkillCaster caster)
    {
        // 휠 윈드 애니메이션 재생
        caster.ExcuteWheelWindAttack(_data.ActvieInfo);
    }
}
