using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SkillController : MonoBehaviour, ISkillCaster
{
    private Coroutine _applyBuffHealCorutine;

    // 버프 적용
    public void ApplyBuff(BuffInfo buffInfo, BuffType type)
    {
        switch(type)
        {
            case BuffType.SpeedUp:
                ApplySpeedUp(buffInfo);
                break;
            case BuffType.Heal:
                ApplyHeal(buffInfo);
                break;
            default:
                break;
        }
    }

    private void ApplySpeedUp(BuffInfo buffInfo)
    {

    }

    private void ApplyHeal(BuffInfo buffInfo)
    {
        // 지속 시간이 설정되어 있는지 확인
        if (buffInfo.ExcuteDuration <= 0)
        {
            Debug.Log("버프 지속 시간이 설정이 되어 있지 않습니다!");
            return;
        }

        if (_applyBuffHealCorutine != null) StopCoroutine(_applyBuffHealCorutine);
        _applyBuffHealCorutine = StartCoroutine(HealCoroutine(buffInfo));
    }

    private IEnumerator HealCoroutine(BuffInfo buffInfo)
    {
        float buffDuration = buffInfo.ExcuteDuration;
        float curDuration = 0.0f;

        // 버프 지속 시간 동안 실행
        while(curDuration < buffDuration)
        {
            curDuration += Time.deltaTime;

            // 플레이어 회복

            yield return null;
        }
    }

    // 휠 윈드 실행
    public void ExcuteWheelWindAttack(ActvieInfo actvieInfo)
    {
        Debug.Log("클릭");
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}
