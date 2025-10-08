using UnityEngine;

public class IdleDog : AIStateBase
{        
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        SetRandomDuration(_aiConfig.DogIdleTime, _aiConfig.DogIdleTime + 15);
    }
        
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        if (UpdateStateTimer())
        {
            SetBool("Sit_b", true);                       
        }
        
        if (_playerDistance < _aiConfig.DogChaseRange)
        {
            SetBool("isChasing", true);          
        }
    }
}
