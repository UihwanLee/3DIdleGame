using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [field:SerializeField] public PlayerSO Data { get; private set; }

    [field:Header("Animation")]
    [field:SerializeField] public PlayerAnimationData AnimationData { get; set; }

    public Animator Animator { get; private set; }

    [field:Header("AI")]
    [field:SerializeField] public NavMeshAgent Agent { get; private set; }

    private PlayerStateMachine _stateMachine;

    [field:SerializeField] public Weapon Weapon { get; private set; }  

    public Health Health { get; private set; }

    public Transform Head { get; private set; }


    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Weapon = GetComponentInChildren<Weapon>(true);

        Agent = GetComponent<NavMeshAgent>();
        Health = GetComponent<Health>();

        Head = transform.GetChild(0).transform;

        _stateMachine = new PlayerStateMachine(this);
    }

    private void Start()
    {
        _stateMachine.ChangeState(_stateMachine.RunState);
        Health.OnDie += OnDie;
    }

    private void Update()
    {
        _stateMachine.HandleInput();
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }

    public void OnDie()
    {
        Animator.SetTrigger("Die");
        enabled = false;
    }
}
