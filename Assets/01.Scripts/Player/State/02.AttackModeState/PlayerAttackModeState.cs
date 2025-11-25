using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackModeState : PlayerBaseState
{
    private float _currentCoolTime;
    private float _attackCoolTime;
    protected bool canAttack;

    public PlayerAttackModeState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AttackModeParameterHash);

        _currentCoolTime = 0f;
        _attackCoolTime = stateMachine.Player.Data.AttackData.AttackCoolTime;

        canAttack = false;
    }


    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackModeParameterHash);
    }

    public override void Update()
    {
        base.Update();
        CheckAttackCoolTime();
    }

    private void CheckAttackCoolTime()
    {
        if (canAttack) return;

        // AttackCoolTime 체크
        _currentCoolTime += Time.deltaTime;
        if(_currentCoolTime >= _attackCoolTime)
        {
            _currentCoolTime = 0f;
            canAttack = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
