using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConiditionHandler : ConditionHandler
{
    private Player _player;

    protected override void Awake()
    {
        base.Awake();

        _player = GetComponent<Player>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void OnDie()
    {
        //_player.Animator.SetTrigger("Die");
        enabled = false;
        _player.Agent.isStopped = true;
        _player.Agent.enabled = false;
    }
}
