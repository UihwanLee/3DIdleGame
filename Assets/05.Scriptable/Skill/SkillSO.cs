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

// Active / Buff 모두 계수로 계산

public interface ISkillCaster
{
    void ExcuteWheelWindAttack();
    void ApplyBuff(float value);

    GameObject GetGameObject();
}

public abstract class SkillSO : ScriptableObject
{
    [field: SerializeField] public string SkillName { get; private set; }
    [field: SerializeField] public Sprite SkillIcon { get; private set; }
    [field: SerializeField] public float SkillCoolTime { get; private set; }
    [field: SerializeField] public SkillTpye Type { get; private set; }

    #region 스킬 로직 실행

    public abstract void Excute(ISkillCaster caster);

    #endregion
}
