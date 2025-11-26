using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActvieInfo
{
    [field: SerializeField][field: Range(1f, 3f)] public int DamageMultiplier { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
}

[CreateAssetMenu(fileName = "ActiveSkill", menuName = "Skill/Active")]
public class ActvieSkillSO : SkillSO
{
    [field: SerializeField] public ActvieInfo ActvieInfo { get; private set; }

    public override void Excute(ISkillCaster caster)
    {
        // 휠 윈드 애니메이션 재생
        caster.ExcuteWheelWindAttack(ActvieInfo);
    }
}
