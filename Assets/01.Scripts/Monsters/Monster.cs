using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [field: SerializeField] public MonsterSO Data { get; private set; }

    [field: Header("Animation")]
    [field: SerializeField] public MonsterAnimData AnimationData { get; set; }

    public Animator Animator { get; private set; }
    [field: Header("AI")]
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }

    private MonsterStateMachine _stateMachine;

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();

        Agent = GetComponent<NavMeshAgent>();

        _stateMachine = new MonsterStateMachine(this);
    }

    private void Start()
    {
        _stateMachine.ChangeState(_stateMachine.IdleState);
    }

    private void Update()
    {
        _stateMachine.Update();
    }
}
