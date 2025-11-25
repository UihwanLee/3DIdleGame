public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }


    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        RunState = new PlayerRunState(this);

        MovementSpeed = player.Data.BaseData.BaseSpeed;
        RotationDamping = player.Data.BaseData.BaseRotationDamping;
    }
}
