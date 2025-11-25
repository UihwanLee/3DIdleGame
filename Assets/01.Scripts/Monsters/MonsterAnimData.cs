using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterAnimData
{
    [SerializeField] private string _baseParameterName = "@Base";
    [SerializeField] private string _idleParameterName = "Idle";

    [SerializeField] private string _chasingModeParameterName = "@ChasingMode";

    [SerializeField] private string _attackModeParameterName = "@AttackMode";
    [SerializeField] private string _attackParmeterName = "Attack";
    [SerializeField] private string _attackIdelParmeterName = "AttackIdle";

    public int BaseParemeterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int ChasingModeParmeterHash { get; private set; }

    public int AttackModeParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int AttackIdleParmeterHash { get; private set; }

    public void Initialize()
    {
        BaseParemeterHash = Animator.StringToHash(_baseParameterName);
        IdleParameterHash = Animator.StringToHash(_idleParameterName);

        ChasingModeParmeterHash = Animator.StringToHash(_chasingModeParameterName);

        AttackModeParameterHash = Animator.StringToHash(_attackModeParameterName);
        AttackParameterHash = Animator.StringToHash(_attackParmeterName);
        AttackIdleParmeterHash = Animator.StringToHash(_attackIdelParmeterName);
    }
}
