using UnityEngine;

public class IdleChicken : AIStateBase
{    
    private float runRange = 5;
    private float _timeToIdle = 3f;
        
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        SetRandomDuration(_timeToIdle, _timeToIdle + 8);
    }
        
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        if (UpdateStateTimer())
        {
            SetBool("isEating", true);
        }
        
        if (_playerDistance < runRange)
        {
            SetBool("isRunning", true);
        }
    }
}
