using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterAttackModeState
{
    public MonsterAttackState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StartAnimation(stateMachine.Monster.AnimationData.AttackModeParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Monster.Animator, "Attack");

        if (normalizedTime > 1f)
        {
            stateMachine.IsAttacking = false;
            stateMachine.ChangeState(stateMachine.AttackIdleState);
            return;
        }
    }
}
