using UnityEngine;

public class PatrolDog : AIStateBase
{
    [SerializeField] string pointsTag;    
    float chaseRange = 15;
    private float _timeToPatrol = 30f;    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        SetRandomDestination(pointsTag);
        SetRandomDuration(_timeToPatrol, _timeToPatrol + 150);       
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
        
        if (_playerDistance < chaseRange)
        {
            SetBool("isChasing", true);            
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_agent.transform.position);
    }
}
