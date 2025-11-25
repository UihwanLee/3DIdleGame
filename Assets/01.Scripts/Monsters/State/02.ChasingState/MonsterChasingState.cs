using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChasingState : MonsterBaseState
{
    private bool _isMovingToTarget = false;
    private float _stoppingDistance = 1f;
    private Transform _head;

    public MonsterChasingState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.ChasingModeParmeterHash);

        _isMovingToTarget = false;

        SetStoppingDistance(_stoppingDistance);
        StartAgent();
        MoveToTarget();

        if(stateMachine.Target.TryGetComponent<Player>(out Player player))
        {
            _head = player.Head;
        }
    }

    private void MoveToTarget()
    {
        // 목표 설정 및 이동
        if (stateMachine.Target != null)
        {
            stateMachine.Monster.Agent.SetDestination(stateMachine.Target.transform.position);
            _isMovingToTarget = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAgent();
        StopAnimation(stateMachine.Monster.AnimationData.ChasingModeParmeterHash);
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

        Vector3 toTarget = (_head.position - stateMachine.Monster.transform.position).normalized;
        float dot = Vector3.Dot(_head.forward, toTarget);

        RotateToTarget(toTarget);

        // 목표와 거리를 비교하여 공격모드로 전환 
        if (stateMachine.Monster.Agent.remainingDistance <= stateMachine.Monster.Agent.stoppingDistance)
        {
            Quaternion targetRotation = Quaternion.LookRotation(toTarget * 10f);
            stateMachine.Monster.transform.rotation = targetRotation;

            _isMovingToTarget = false;
            StopAgent();
            stateMachine.ChangeState(stateMachine.AttackIdleState);
        }
    }

    private void RotateToTarget(Vector3 toTarget)
    {
        Quaternion targetRotation = Quaternion.LookRotation(toTarget);
        stateMachine.Monster.transform.rotation = Quaternion.Slerp(stateMachine.Monster.transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    protected bool IsInAttackRange()
    {
        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.Monster.Data.AttackRange * stateMachine.Monster.Data.AttackRange;
    }
}
