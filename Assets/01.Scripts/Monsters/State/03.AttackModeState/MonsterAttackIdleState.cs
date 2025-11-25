using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackIdleState : MonsterAttackModeState
{
    public MonsterAttackIdleState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.AttackIdleParmeterHash);

        // Agent 설정
        StopAgent();
        SetSpeed(0f);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.AttackIdleParmeterHash);
    }

    public override void Update()
    {
        base.Update();
        CheckAttack();
    }

    private void CheckAttack()
    {
        if (stateMachine.IsAttacking)
        {
            // 콤보 랜덤으로 설정
            //stateMachine.ComboIndex = Random.Range(0, 3);

            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }
}
