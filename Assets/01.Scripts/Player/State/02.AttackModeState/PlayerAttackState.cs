using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAttackModeState
{
    private bool _alreadAppliedCombo;

    AttackInfoData attackInfoData;

    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        _alreadAppliedCombo = false;
        stateMachine.IsAttacking = true;

        int comboindex = stateMachine.CurrentComboIndex;
        attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfoData(comboindex);
        stateMachine.Player.Animator.SetInteger("Combo", comboindex);
        StartAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if(normalizedTime < 1f)
        {
            // 콤보 시도
            if (normalizedTime >= attackInfoData.ComboTransitionTime)
                TryComboAttack();
        }
        else
        {
            if(_alreadAppliedCombo)
            {
                stateMachine.CurrentComboIndex += 1;
                stateMachine.ChangeState(stateMachine.AttackState);
            }
            else
            {
                stateMachine.CurrentComboIndex = 0;
                stateMachine.IsAttacking = false;
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }

    private void TryComboAttack()
    {
        if (_alreadAppliedCombo) return;

        if (attackInfoData.ComboStateIndex == -1) return;

        if (!stateMachine.IsAttacking) return;

        if (stateMachine.CurrentComboIndex >= stateMachine.ComboIndex) return;

        _alreadAppliedCombo = true;
    }
}
