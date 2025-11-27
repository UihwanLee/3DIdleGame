using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Active / Buff 모두 계수로 계산

public interface ISkillCaster
{
    void ExcuteWheelWindAttack(ActvieInfo actvieInfo);
    void ApplyBuff(BuffInfo buffInfo, BuffType type);

    GameObject GetGameObject();
}

public abstract class Skill
{
    protected SkillSO _data;

    public Skill(SkillSO data)
    {
        this._data = data;
    }

    public virtual SkillSO Data { get { return _data; } }

    #region 스킬 로직 실행

    public abstract void Excute(ISkillCaster caster);

    #endregion
}
