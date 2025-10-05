using UnityEngine;

public class ChaseDog : AIStateBase
{
    [SerializeField] float speedChase;
    float chaseRange = 20; //разстояние через которое не будет приследовать
    float attackRange = 1.5f;  
        
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        _agent.speed = speedChase;
    }
        
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_player.position);
        
        if (_playerDistance < attackRange)
        {
            SetBool("isBarking", true);            
        }

        if (_playerDistance > chaseRange)
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
