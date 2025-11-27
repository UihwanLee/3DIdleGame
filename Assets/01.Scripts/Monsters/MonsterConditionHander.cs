using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterConditionHander : ConditionHandler
{
    private Monster monster;

    protected override void Awake()
    {
        base.Awake();

        monster = GetComponent<Monster>();
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void OnDie()
    {
        monster.Animator.SetTrigger("Die");
        enabled = false;
        monster.Agent.isStopped = true;
        monster.Agent.enabled = false;

        // 플레이어에게 보상 주기
        monster.StateMachine.Target.AddCondition(ConditionType.Exp, monster.Data.RewardInfo.GainExp);
        monster.StateMachine.Target.AddCondition(ConditionType.Gold, monster.Data.RewardInfo.GainGold);

        float value = Random.Range(0f, 1f);
        foreach (var dropItem in monster.Data.RewardInfo.DropItemTables)
        {
            if (dropItem.DropPercentage >= value)
            {
                Debug.Log("Value:" + value);
                monster.StateMachine.Target.GetComponent<Player>().GetItem(dropItem.DropItem.GetComponent<ItemData>());
            }
        }
    }
}
