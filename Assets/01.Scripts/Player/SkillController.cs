using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class SkillController : MonoBehaviour, ISkillCaster
{
    private Player player;

    private Coroutine _applyBuffSpeedUpCoroutine;
    private Coroutine _applyBuffHealCoroutine;
    private Coroutine _activeWheelWindCoroutine;

    private List<Collider> alreadyHitColliders = new List<Collider>();

    // 기즈모를 그릴 때 사용할 OverlapCapsule의 파라미터들
    private Vector3 _gizmoPoint1;
    private Vector3 _gizmoPoint2;
    private float _gizmoRadius;
    private bool _drawGizmo = false; // 기즈모를 그릴지 말지 결정하는 플래그

    public bool ActvingSkill { get; set; }

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
        // 지속 시간이 설정되어 있는지 확인
        if (buffInfo.ExcuteDuration <= 0)
        {
            Debug.Log("버프 지속 시간이 설정이 되어 있지 않습니다!");
            return;
        }

        if (_applyBuffSpeedUpCoroutine != null) StopCoroutine(_applyBuffSpeedUpCoroutine);
        _applyBuffSpeedUpCoroutine = StartCoroutine(SpeedUpCoroutine(buffInfo));
    }

    private IEnumerator SpeedUpCoroutine(BuffInfo buffInfo)
    {
        float buffDuration = buffInfo.ExcuteDuration;
        float curDuration = 0.0f;

        // 버프 지속 시간 동안 실행
        while (curDuration < buffDuration)
        {
            curDuration += Time.deltaTime;

            float speedAmount = (int)(buffInfo.ValueMultiplier * player.Condition.Speed);

            // 플레이어 속도 증가
            player.Condition.SetCondition(ConditionType.Speed, speedAmount);

            player.Agent.speed = speedAmount;

            yield return new WaitForSeconds(buffInfo.TickInterval);

            curDuration += buffInfo.TickInterval;
        }

        // 버프가 꺼지면 원래 속도로 돌아가기
        player.Agent.speed = player.Condition.ConditionInfoSO.BaseSpeed;
    }

    private void ApplyHeal(BuffInfo buffInfo)
    {
        // 지속 시간이 설정되어 있는지 확인
        if (buffInfo.ExcuteDuration <= 0)
        {
            Debug.Log("버프 지속 시간이 설정이 되어 있지 않습니다!");
            return;
        }

        if (_applyBuffHealCoroutine != null) StopCoroutine(_applyBuffHealCoroutine);
        _applyBuffHealCoroutine = StartCoroutine(HealCoroutine(buffInfo));
    }

    private IEnumerator HealCoroutine(BuffInfo buffInfo)
    {
        float buffDuration = buffInfo.ExcuteDuration;
        float curDuration = 0.0f;

        // 버프 지속 시간 동안 실행
        while (curDuration < buffDuration)
        {
            curDuration += Time.deltaTime;

            int healAmount = (int)(buffInfo.ValueMultiplier * player.Condition.Hp);

            // 플레이어 회복
            player.Condition.AddCondition(ConditionType.Hp, healAmount);

            // Damage Text 표시
            FloatingTextPoolManager.Instance.SpawnText(TextType.Damage, $"+{healAmount}", player.Head.transform, Color.green);

            yield return new WaitForSeconds(buffInfo.TickInterval);

            curDuration += buffInfo.TickInterval;
        }
    }

    // 휠 윈드 실행
    public void ExcuteWheelWindAttack(ActvieInfo actvieInfo)
    {
        player.Animator.SetTrigger("WheelWind");

        // 지속 시간이 설정되어 있는지 확인
        if (actvieInfo.ExcuteDuration <= 0)
        {
            Debug.Log("버프 지속 시간이 설정이 되어 있지 않습니다!");
            return;
        }

        if (_activeWheelWindCoroutine != null) StopCoroutine(_activeWheelWindCoroutine);
        _activeWheelWindCoroutine = StartCoroutine(WheelWindCoroutine(actvieInfo));

        //player.Animator.SetTrigger("Skill");
        player.Animator.SetBool(player.AnimationData.SkillWheelWindParameterHash, true);
    }

    private IEnumerator WheelWindCoroutine(ActvieInfo activeInfo)
    {
        float buffDuration = activeInfo.ExcuteDuration;
        float curDuration = 0.0f;

        player.Weapon.gameObject.SetActive(true);

        alreadyHitColliders.Clear();

        // 플레이어 collider를 가져와서 체크
        CapsuleCollider weaponCollider = player.GetComponent<CapsuleCollider>();

        ActvingSkill = true;
        _drawGizmo = true;

        // 스킬 지속 시간 동안 실행
        while (curDuration < buffDuration)
        {
            curDuration += Time.deltaTime;

            // 캡슐의 Transform (Weapon)
            Transform weaponTransform = weaponCollider.transform;

            float radius = weaponCollider.radius * weaponTransform.localScale.x * 3f;
            float height = weaponCollider.height * weaponTransform.localScale.y;

            // 캡슐의 양 끝점 계산 (Local to World)
            Vector3 point1Local = weaponCollider.center + Vector3.up * (height / 2f - radius);
            Vector3 point2Local = weaponCollider.center - Vector3.up * (height / 2f - radius);

            Vector3 point1World = weaponTransform.TransformPoint(point1Local);
            Vector3 point2World = weaponTransform.TransformPoint(point2Local);

            _gizmoPoint1 = point1World;
            _gizmoPoint2 = point2World;
            _gizmoRadius = radius;

            Collider[] hitColliders = Physics.OverlapCapsule(
                point1World,
                point2World,
                radius,
                activeInfo.TargetLayer
            );

            foreach (Collider other in hitColliders)
            {
                if (other == this.transform.GetComponent<CapsuleCollider>()) continue;

                if (alreadyHitColliders.Contains(other)) continue;

                alreadyHitColliders.Add(other);

                if (other.TryGetComponent(out ConditionHandler condition))
                {
                    // 데미지 적용
                    condition.SubCondition(ConditionType.Hp, player.Condition.Atk * activeInfo.DamageMultiplier, Color.red);
                }
            }

            yield return new WaitForSeconds(activeInfo.TickInterval);

            curDuration += activeInfo.TickInterval;
        }

        ActvingSkill = false;
        player.Weapon.gameObject.SetActive(false);
        player.Animator.SetBool(player.AnimationData.SkillWheelWindParameterHash, false);

        _drawGizmo = false;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    private void OnDrawGizmos()
    {
        if (_drawGizmo)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(_gizmoPoint1, _gizmoRadius);
            Gizmos.DrawWireSphere(_gizmoPoint2, _gizmoRadius);

            Vector3 topDirection = (_gizmoPoint1 - _gizmoPoint2).normalized;
            Vector3 side = Vector3.Cross(topDirection, Vector3.up).normalized * _gizmoRadius; 
            if (side == Vector3.zero) side = Vector3.right * _gizmoRadius; 

            Gizmos.DrawLine(_gizmoPoint1 + side, _gizmoPoint2 + side);
            Gizmos.DrawLine(_gizmoPoint1 - side, _gizmoPoint2 - side);
        }
    }
}
