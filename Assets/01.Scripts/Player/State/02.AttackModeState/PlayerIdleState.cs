
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
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
        //CheckDistance();
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

    private void CheckDistance()
    {
        if (_target == null) Debug.Log("타켓이 널임");

        // 만약 타켓과의 거리가 멀다면 다시 추척모드
        if (Vector3.Distance(_target.transform.position, stateMachine.Player.transform.position) < _stoppingDistance)
        {
            stateMachine.ChangeState(stateMachine.RunState);
        }
    }
}
