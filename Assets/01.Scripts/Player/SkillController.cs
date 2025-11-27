using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SkillController : MonoBehaviour, ISkillCaster
{
    private Player player;
    private Coroutine _applyBuffHealCorutine;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

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
        float tickInterval = 0.5f;

        // 버프 지속 시간 동안 실행
        while (curDuration < buffDuration)
        {
            curDuration += Time.deltaTime;

            int healAmount = (int)(buffInfo.ValueMultiplier * player.Condition.Hp);

            // 플레이어 회복
            player.Condition.AddCondition(ConditionType.Hp, healAmount);

            // Damage Text 표시
            FloatingTextPoolManager.Instance.SpawnText(TextType.Damage, $"+{healAmount}", player.Head.transform, Color.green);

            yield return new WaitForSeconds(tickInterval);

            curDuration += tickInterval;
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
