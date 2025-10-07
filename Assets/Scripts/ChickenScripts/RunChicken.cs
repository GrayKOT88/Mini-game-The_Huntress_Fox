using UnityEngine;

public class RunChicken : AIStateBase
{
    private const float MIN_SPEED = 3.5f;
    private const float MAX_SPEED = 5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);        
        _agent.speed = Random.Range(MIN_SPEED, MAX_SPEED);        
    }
        
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 runDirection = (animator.transform.position - _player.position).normalized;

        if (runDirection != Vector3.zero)
            _agent.SetDestination(animator.transform.position + runDirection * _chaseRange);
        
        if (_playerDistance > _chaseRange)
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
