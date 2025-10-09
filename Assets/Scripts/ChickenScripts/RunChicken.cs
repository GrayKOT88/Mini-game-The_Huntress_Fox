using UnityEngine;

public class RunChicken : AIStateBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);        
        _agent.speed = Random.Range(_aiConfig.ChickenMinSpeed, _aiConfig.ChickenMaxSpeed);        
    }
        
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 runDirection = (animator.transform.position - _player.position).normalized;

        if (runDirection != Vector3.zero)
            _agent.SetDestination(animator.transform.position + runDirection * _aiConfig.ChickenRunRange);
        
        if (_playerDistance > _aiConfig.ChickenChaseRange)
        {
            SetBool("isRunning", false);
            SetBool("isWalking", false);
        }        
    }
        
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_agent.transform.position);
        _agent.speed = 1;
    }
}
