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
    [field: SerializeField] public Weapon Weapon { get; private set; }
    public Transform Head { get; private set; }

    public MonsterConditionHander Condition { get; private set; }

    private void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();

        Agent = GetComponent<NavMeshAgent>();

        Head = transform.GetChild(0).transform;

        Weapon = GetComponentInChildren<Weapon>(true);

        Condition = GetComponentInChildren<MonsterConditionHander>();

        _stateMachine = new MonsterStateMachine(this);
    }

    private void Start()
    {
        _stateMachine.ChangeState(_stateMachine.IdleState);

        // 데미지 표시는 Head Transform 위치를 따라감

        Condition = GetComponentInChildren<MonsterConditionHander>();
        if (Condition == null) Debug.Log("Condition이 없음");
        Condition.SetDamageTransform(Head);
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public MonsterStateMachine StateMachine { get { return _stateMachine; } }
}
