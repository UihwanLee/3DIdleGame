public class PlayerRunState : PlayerAutoFindState
{
    public PlayerRunState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAgent();
        SetSpeed(stateMachine.Player.Data.BaseData.BaseSpeed * stateMachine.Player.Data.BaseData.RunSpeedModifier);
        StartAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAgent();
        StopAnimation(stateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Update()
    {
        base.Update();
    }
}
