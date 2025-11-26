using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string _baseParameterName = "@Base";
    [SerializeField] private string _autoFindParameterName = "@AutoFind";
    [SerializeField] private string _walkParameterName = "Walk";
    [SerializeField] private string _runParameterName = "Run";

    [SerializeField] private string _attackModeParameterName = "@AttackMode";
    [SerializeField] private string _idleParameterName = "Idle";
    [SerializeField] private string _attackParmeterName = "ComboAttack";


    public int BaseParemeterHash { get; private set; }
    public int AutoFindParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }

    public int AttackModeParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }

    public void Initialize()
    {
        BaseParemeterHash = Animator.StringToHash(_baseParameterName);
        AutoFindParameterHash = Animator.StringToHash(_autoFindParameterName);
        WalkParameterHash = Animator.StringToHash(_walkParameterName);
        RunParameterHash = Animator.StringToHash(_runParameterName);

        AttackModeParameterHash = Animator.StringToHash(_attackModeParameterName);
        IdleParameterHash = Animator.StringToHash(_idleParameterName);
        AttackParameterHash = Animator.StringToHash(_attackParmeterName);
    }

}
