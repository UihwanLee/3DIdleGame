using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBaseState : IState
{
    protected MonsterStateMachine stateMachine;

    public MonsterBaseState(MonsterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
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
        stateMachine.Monster.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        stateMachine.Monster.Animator.SetBool(animatorHash, false);
    }

    protected void StartAgent()
    {
        stateMachine.Monster.Agent.isStopped = false;
    }

    protected void StopAgent()
    {
        if (!stateMachine.Monster.Agent.enabled) return;

        stateMachine.Monster.Agent.isStopped = true;
    }

    protected void SetStoppingDistance(float distance)
    {
        stateMachine.Monster.Agent.stoppingDistance = distance;
    }

    protected void SetSpeed(float speed)
    {
        stateMachine.Monster.Agent.speed = speed;
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
            return nextInfo.normalizedTime;

        if (currentInfo.IsTag(tag))
            return currentInfo.normalizedTime;

        return -1f; // tag가 아니라면 -1로 리턴하여 디버깅
    }

    protected float GetNormalizedTimeByName(Animator animator, string fullStateName)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsName(fullStateName))
            return nextInfo.normalizedTime;

        if (currentInfo.IsName(fullStateName))
            return currentInfo.normalizedTime;

        return -1f;
    }

    protected bool IsInChaseRange()
    {
        if (stateMachine.Target == null) Debug.Log("Target이 없음");

        float playerDistanceSqr = (stateMachine.Target.transform.position - stateMachine.Monster.transform.position).sqrMagnitude;
        return playerDistanceSqr <= stateMachine.Monster.Data.ChaseRange;
    }
}
