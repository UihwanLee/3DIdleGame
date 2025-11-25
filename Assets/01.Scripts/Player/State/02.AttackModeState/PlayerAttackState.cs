using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAttackState : PlayerAttackModeState
{
    private bool _alreadAppliedCombo;
    private bool _alreadyAppliedDealing;

    AttackInfoData attackInfoData;

    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        _alreadAppliedCombo = false;
        stateMachine.IsAttacking = true;

        _alreadyAppliedDealing = false;

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

            PlayerAttackData attackData = stateMachine.Player.Data.AttackData;
            if (!_alreadyAppliedDealing && normalizedTime >= attackData.GetAttackInfoData(stateMachine.CurrentComboIndex).Dealing_Start_TransitionTime)
            {
                RotateToTarget();
                stateMachine.Player.Weapon.SetAttack(attackData.GetAttackInfoData(stateMachine.CurrentComboIndex).Damage);
                stateMachine.Player.Weapon.gameObject.SetActive(true);
                _alreadyAppliedDealing = true;
            }

            if (_alreadyAppliedDealing && normalizedTime >= stateMachine.Player.Data.AttackData.GetAttackInfoData(stateMachine.CurrentComboIndex).Dealing_End_TransitionTime)
            {
                stateMachine.Player.Weapon.gameObject.SetActive(false);
            }
        }
        else
        {
            if(_alreadAppliedCombo)
            {
                stateMachine.CurrentComboIndex += 1;
                stateMachine.ChangeState(stateMachine.AttackState);
                return;
            }
            else
            {
                stateMachine.CurrentComboIndex = 0;
                stateMachine.IsAttacking = false;
                stateMachine.ChangeState(stateMachine.IdleState);
                return;
            }
        }
    }

    private void RotateToTarget()
    {
        if (stateMachine.Target != null)
        {
            Transform head = stateMachine.Target.GetComponent<Monster>().Head;
            Vector3 toTarget = (head.position - stateMachine.Player.transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(toTarget);
            stateMachine.Player.transform.rotation = targetRotation;
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
