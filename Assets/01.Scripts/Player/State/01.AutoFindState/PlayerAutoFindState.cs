using UnityEngine;

public class PlayerAutoFindState : PlayerBaseState
{
    private Transform _target;
    private Transform _head;
    private bool _isMovingToTarget = false;
    private float _stoppingDistance = 2f;

    public PlayerAutoFindState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _isMovingToTarget = false;

        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AutoFindParameterHash);

        SetStoppingDistance(_stoppingDistance);
        SetTargetMonster();
        MoveToTarget();
    }

    private void SetTargetMonster()
    {
        _target = null;

        // 근방의 몬스터를 찾아서 Destination 찾기
        Monster monster = GameObject.FindFirstObjectByType<Monster>();
        if (monster != null)
        {
            _target = monster.transform;
            stateMachine.Target = _target.GetComponent<Health>();

            if(_target.TryGetComponent<Monster>(out Monster enemy))
            {
                _head = enemy.Head;
            }
        }
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

        // 목표와 거리를 비교하여 공격모드로 전환 
        if (stateMachine.Player.Agent.remainingDistance <= stateMachine.Player.Agent.stoppingDistance)
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
