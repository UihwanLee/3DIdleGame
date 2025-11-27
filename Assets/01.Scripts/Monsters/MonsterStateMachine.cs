using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateMachine : StateMachine
{
    public Monster Monster { get; }


    public ConditionHandler Target { get; private set; }
    public MonsterIdleState IdleState { get; }
    public MonsterAttackState AttackState { get; }
    public MonsterChasingState ChasingState { get; }
    public MonsterAttackIdleState AttackIdleState { get; }

    public bool IsAttacking { get; set; }

    public MonsterStateMachine(Monster monster)
    {
        this.Monster = monster;
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<ConditionHandler>();

        IdleState = new MonsterIdleState(this);
        AttackState = new MonsterAttackState(this);
        ChasingState = new MonsterChasingState(this);
        AttackIdleState = new MonsterAttackIdleState(this);
    }

}
