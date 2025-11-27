using System;
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

    [field: Header("Component")]
    [field: SerializeField] public SkillController SkillController { get; private set; }

    public Transform Head { get; private set; }

    public PlayerConiditionHandler Condition { get; private set; }

    public event Action<ItemData> ItemGetEvent; // 플레이어가 아이템을 흭득했을 때 이벤트

    public ItemData CurrentWeapon { get; set; }

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        Weapon = GetComponentInChildren<Weapon>(true);

        Agent = GetComponent<NavMeshAgent>();

        Head = transform.GetChild(0).transform;
        SkillController = GetComponent<SkillController>();

        _stateMachine = new PlayerStateMachine(this);

        Condition = GetComponent<PlayerConiditionHandler>();
    }

    private void Start()
    {
        _stateMachine.ChangeState(_stateMachine.RunState);

        // 데미지 표시는 Head Transform 위치를 따라감
        Condition.SetDamageTransform(Head);
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

    public void GetItem(ItemData item)
    {
        ItemGetEvent?.Invoke(item);
    }

    public PlayerStateMachine StateMachine { get { return _stateMachine; } }
}
