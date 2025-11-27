using UnityEngine;

public class PlayerAutoFindState : PlayerBaseState
{
    private Transform _head;
    private bool _isMovingToTarget = false;

    public PlayerAutoFindState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _isMovingToTarget = false;

        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AutoFindParameterHash);

        Debug.Log("AutoFindMode 전환");

        SetStoppingDistance(_stoppingDistance);
        SetTargetMonster();
        MoveToTarget();
    }

    private void SetTargetMonster()
    {
        _target = null;

        // 근방의 몬스터를 찾아서 Destination 찾기
        Monster monster = FindNearByMonster();
        if (monster != null)
        {
            _target = monster.transform;
            stateMachine.Target = _target.GetComponent<ConditionHandler>();

            if(_target.TryGetComponent<Monster>(out Monster enemy))
            {
                _head = enemy.Head;
            }
        }
        else
        {
            Debug.Log("Stage 클리어");

            // Stage 선택 UI 보여주기
            GameManager.Instance.OpenStageSelectUI();
        }
    }

    private Monster FindNearByMonster()
    {
        // OverlapSphere 반경
        float searchRadius = 50f;
        Collider[] hitColliders = Physics.OverlapSphere(stateMachine.Player.transform.position, searchRadius);

        Monster nearestMonster = null;
        float shortestDistanceSqr = searchRadius * searchRadius;

        foreach (Collider hitCollider in hitColliders)
        {
            // hitCollider에서 Monster가 있는지 확인
            if (hitCollider.TryGetComponent<Monster>(out Monster monster))
            {
                // 자기 자신 제외
                if (monster.transform == stateMachine.Player.transform) continue;

                // 죽은 monster 제외
                if (monster.GetComponent<ConditionHandler>().IsDead) continue;

                // 반경 내에 있는 몬스터들 중에서 가장 가까운 몬스터 찾기
                float distanceSqr = (monster.transform.position - stateMachine.Player.transform.position).sqrMagnitude;

                if (distanceSqr < shortestDistanceSqr)
                {
                    shortestDistanceSqr = distanceSqr;
                    nearestMonster = monster;
                }
            }
        }

        return nearestMonster;
    }

    private void MoveToTarget()
    {
        // 목표 설정 및 이동
        if(_target != null)
        {
            stateMachine.Player.Agent.SetDestination(_target.position);
            _isMovingToTarget = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AutoFindParameterHash);
        _isMovingToTarget = false;
    }

    public override void Update()
    {
        base.Update();
        CheckDistanceTarget();
    }

    private void CheckDistanceTarget()
    {
        if (!_isMovingToTarget) return;

        Vector3 toTarget = (_head.position - stateMachine.Player.transform.position).normalized;
        float dot = Vector3.Dot(_head.forward, toTarget);

        stateMachine.Player.Agent.SetDestination(_target.position);

        // 목표와 거리를 비교하여 공격모드로 전환 
        //stateMachine.Player.Agent.remainingDistance <= stateMachine.Player.Agent.stoppingDistance
        if (Vector3.Distance(_target.transform.position, stateMachine.Player.transform.position) < _stoppingDistance)
        {
            // Target을 바라보도록 방향 전환
            if (dot > -0.5f)
            {
                RotateToTarget(toTarget);
            }
            else
            {
                Quaternion targetRotation = Quaternion.LookRotation(toTarget * 10f);
                stateMachine.Player.transform.rotation = targetRotation;

                _isMovingToTarget = false;
                StopAgent();
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    private void RotateToTarget(Vector3 toTarget)
    {
        Quaternion targetRotation = Quaternion.LookRotation(toTarget);
        stateMachine.Player.transform.rotation = Quaternion.Slerp(stateMachine.Player.transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
