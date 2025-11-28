# 3DIdleGame
3D 방치형 게임 개인 프로젝트입니다.

<img width="235" height="414" alt="3DIdle" src="https://github.com/user-attachments/assets/5ce28f8a-295b-43cd-9717-d46c033ab7af" />

## 1. 프로젝트 소개

'논스톱 나이트' 게임 레퍼런스 기반한 3D 방치형 게임입니다.

## 2. 프로젝트 구현 내용

1. AI Navigation을 이용, FSM 구조를 통한 AI 자동 전투 움직임 구현
2. ObjectPool 기법을 활용한 데미지 FloatingText UI 표기 
3. 인터페이스(ISkillCaster)를 이용한 다양한 Skill 구현
4. 확장 메서드(FindChild<T>)와 Resources 동적 로딩을 이용한 필드 초기화 & 동적 로딩
5. Condition 클래스를 통한 Player Stat과 (체력,골드,경험치) 통합 관리

## 3. 사용 기술

>> 확장 메서드를 활용한 필드 초기화
<pre>
<code>
public static class FindExtension 
{
    public static T FindChild<T>(this Transform transform, string name) where T : Component
    {
        T[] children = transform.GetComponentsInChildren<T>(includeInactive: true);
        foreach (T child in children)
        {
            if (child.gameObject.name == name)
            {
                return child;
            }
        }
        return null;
    }
}
</code>
</pre>

>> ISkillCaster를 활용한 스킬 구현
<pre>
<code>
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
</code>
</pre>
