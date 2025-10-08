using UnityEngine;

public class PatrolDog : AIStateBase
{
    [SerializeField] string pointsTag;    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        SetRandomDestination(pointsTag);
        SetRandomDuration(_aiConfig.DogPatrolTime, _aiConfig.DogPatrolTime + 150);       
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {      
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            SetRandomDestination(pointsTag);
        }
               
        if (UpdateStateTimer())
        {
            SetBool("isPatrolling", false);           
        }
        
        if (_playerDistance < _aiConfig.DogChaseRange)
        {
            SetBool("isChasing", true);            
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_agent.transform.position);
    }
}
