using UnityEngine;

public class SittingDog : AIStateBase
{        
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        SetRandomDuration(_aiConfig.DogSitTime, _aiConfig.DogSitTime + 20);
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        if (UpdateStateTimer())
        {            
            SetBool("Sit_b", false);            
            SetBool("isPatrolling", true);            
        }
        
        if (_playerDistance < _aiConfig.DogChaseRange)
        {
            SetBool("Sit_b", false);
            SetBool("isChasing", true);
        }
    }
}
