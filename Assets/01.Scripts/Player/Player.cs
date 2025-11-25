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


    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();

        Agent = GetComponent<NavMeshAgent>();

        _stateMachine = new PlayerStateMachine(this);
        _stateMachine.ChangeState(_stateMachine.RunState);
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
}
