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

    public Transform Head { get; private set; }

    public Health Health { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();

        Agent = GetComponent<NavMeshAgent>();

        Head = transform.GetChild(0).transform;

        Health = GetComponent<Health>();

        _stateMachine = new MonsterStateMachine(this);
    }

    private void Start()
    {
        _stateMachine.ChangeState(_stateMachine.IdleState);
        Health.OnDie += OnDie;
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void OnDie()
    {
        Animator.SetTrigger("Die");
        enabled = false;
        Agent.isStopped = true;
        Agent.enabled = false;
    }
}
