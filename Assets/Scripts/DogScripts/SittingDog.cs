using UnityEngine;

public class SittingDog : AIStateBase
{    
    float chaseRange = 15;
    private float _timeToSitting = 5f;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        SetRandomDuration(_timeToSitting, _timeToSitting + 20);
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        if (UpdateStateTimer())
        {            
            SetBool("Sit_b", false);            
            SetBool("isPatrolling", true);            
        }
        
        if (_playerDistance < chaseRange)
        {
            SetBool("Sit_b", false);
            SetBool("isChasing", true);
        }
    }
}
