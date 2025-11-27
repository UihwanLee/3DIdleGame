using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected readonly PlayerBaseData baseData;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        baseData = stateMachine.Player.Data.BaseData;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void HandleInput()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        
    }

    public virtual void Update()
    {
        
    }

    protected void StartAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Player.Animator.SetBool(animatorHash, false);
    }

    protected void StartAgent()
    {
        stateMachine.Player.Agent.isStopped = false;
    }

    protected void StopAgent()
    {
        stateMachine.Player.Agent.isStopped = true;
    }

    protected void SetStoppingDistance(float distance)
    {
        stateMachine.Player.Agent.stoppingDistance = distance;
    }

    protected void SetSpeed(float speed)
    {
        stateMachine.Player.Agent.speed = speed;
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        // 전환되고 있을 때 && 다음 애니메이션이 tag
        if(animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        // 전환되고 있지 않을 때 %% 현재 애니메이션이 tag
        else if(!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;  
        }
        else
        {
            return 0f;
        }
    }

    protected float CaculateDamage(float damageMultiplier)
    {
        // 플레이어 기본 공격력 * (스킬 계수) + 장착 무기 공격력
        return stateMachine.Player.Condition.Atk * damageMultiplier + stateMachine.Player.CurrentWeapon.data.weaponInfo.baseDamage;
    }
}
