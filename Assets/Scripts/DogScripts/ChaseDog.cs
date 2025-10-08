using UnityEngine;

public class ChaseDog : AIStateBase
{
    [SerializeField] float speedChase;    
        
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        _agent.speed = speedChase;
    }
        
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_player.position);
        
        if (_playerDistance < _aiConfig.DogAttackRange)
        {
            SetBool("isBarking", true);            
        }

        if (_playerDistance > _aiConfig.DogStopChaseDistance)
        {
            SetBool("isChasing", false);                      
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_agent.transform.position);
        _agent.speed = 1;
    }
}
