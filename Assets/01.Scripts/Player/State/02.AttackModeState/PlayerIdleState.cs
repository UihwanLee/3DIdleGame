
using UnityEngine;

public class PlayerIdleState : PlayerAttackModeState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.IdleParameterHash);

        // Agent 설정
        StopAgent();
        SetSpeed(0f);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
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
            stateMachine.ComboIndex = Random.Range(0, 3);

            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }
}
