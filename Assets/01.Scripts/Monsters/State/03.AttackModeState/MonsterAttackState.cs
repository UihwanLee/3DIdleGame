using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterAttackModeState
{
    private bool _alreadyAppliedDealing;

    public MonsterAttackState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StartAnimation(stateMachine.Monster.AnimationData.AttackModeParameterHash);
        StartAnimation(stateMachine.Monster.AnimationData.AttackParameterHash);

        _alreadyAppliedDealing = false;
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


        if (normalizedTime < 1f)
        {
            if(!_alreadyAppliedDealing && normalizedTime >= stateMachine.Monster.Data.Dealing_Start_TransitionTime)
            {
                _alreadyAppliedDealing = true;
            }

            if(_alreadyAppliedDealing && normalizedTime >= stateMachine.Monster.Data.Dealing_End_TransitionTime)
            {

            }
        }
        else
        {
            stateMachine.IsAttacking = false;
            stateMachine.ChangeState(stateMachine.AttackIdleState);
            return;
        }
    }
}
