using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackModeState : MonsterBaseState
{
    private float _currentCoolTime;
    private float _attackCoolTime;

    public MonsterAttackModeState(MonsterStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Monster.AnimationData.AttackModeParameterHash);

        _currentCoolTime = 0f;
        _attackCoolTime = stateMachine.Monster.Data.AttackCoolTime;

        stateMachine.IsAttacking = false;
    }


    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Monster.AnimationData.AttackModeParameterHash);
    }

    public override void Update()
    {
        base.Update();
        CheckAttackCoolTime();
    }

    private void CheckAttackCoolTime()
    {
        if (stateMachine.IsAttacking) return;

        // AttackCoolTime 체크
        _currentCoolTime += Time.deltaTime;
        if (_currentCoolTime >= _attackCoolTime)
        {
            _currentCoolTime = 0f;
            stateMachine.IsAttacking = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
