using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : MonsterBaseState
{
    public MonsterIdleState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.BaseParemeterHash);
        StartAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.BaseParemeterHash);
        StopAnimation(stateMachine.Monster.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if(IsInChaseRange())
        {
            stateMachine.ChangeState(stateMachine.ChasingState);
            return;
        }
    }
}
