public class PlayerStateMachine : StateMachine
{
    public Player Player { get; }

    public float MovementSpeed { get; private set; }
    public float RotationDamping { get; private set; }
    public float MovementSpeedModifier { get; private set; }

    public bool IsAttacking { get; set; }
    public int ComboIndex { get; set; }
    public int CurrentComboIndex { get; set; }

    public ConditionHandler Target { get; set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }


    public PlayerStateMachine(Player player)
    {
        this.Player = player;

        IdleState = new PlayerIdleState(this);
        RunState = new PlayerRunState(this);
        AttackState = new PlayerAttackState(this);

        MovementSpeed = player.Data.BaseData.BaseSpeed;
        RotationDamping = player.Data.BaseData.BaseRotationDamping;
    }
}
