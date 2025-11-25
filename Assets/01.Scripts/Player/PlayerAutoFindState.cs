using UnityEngine;

public class PlayerAutoFindState : PlayerBaseState
{
    private Transform target;
    private bool isMovingToTarget = false;

    public PlayerAutoFindState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        isMovingToTarget = false;

        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AutoFindParameterHash);
        SetTargetMonster();
        MoveToTarget();
    }

    private void SetTargetMonster()
    {
        target = null;

        // 근방의 몬스터를 찾아서 Destination 찾기
        Monster monster = GameObject.FindFirstObjectByType<Monster>();
        if (monster != null)
        {
            target = monster.transform;
        }
    }

    private void MoveToTarget()
    {
        // 목표 설정 및 이동
        if(target != null)
        {
            stateMachine.Player.Agent.SetDestination(target.position);
            isMovingToTarget = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AutoFindParameterHash);
        isMovingToTarget = false;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
