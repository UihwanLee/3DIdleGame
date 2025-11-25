using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAttackModeState
{
    private float _attackDuration;
    private float _currentDuration;
    private Coroutine _attackCoroutine;

    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        _attackDuration = 0.4f;
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);

        _currentDuration = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        canAttack = false;
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();
        Attack();
    }

    private void Attack()
    {
        _currentDuration += Time.deltaTime;
        if (_currentDuration > _attackDuration)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}
