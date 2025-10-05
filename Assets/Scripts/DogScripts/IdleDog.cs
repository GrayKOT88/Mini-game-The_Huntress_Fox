using UnityEngine;

public class IdleDog : AIStateBase
{    
    float chaseRange = 15;
    private float _timeToIdle = 3f;
        
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        SetRandomDuration(_timeToIdle, _timeToIdle + 15);
    }
        
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        if (UpdateStateTimer())
        {
            SetBool("Sit_b", true);                       
        }
        
        if (_playerDistance < chaseRange)
        {
            SetBool("isChasing", true);          
        }
    }
}
