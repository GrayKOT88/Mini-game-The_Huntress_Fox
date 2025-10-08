using UnityEngine;

public class IdleChicken : AIStateBase
{                
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        SetRandomDuration(_aiConfig.ChickenIdleTime, _aiConfig.ChickenIdleTime + 8);
    }
        
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        if (UpdateStateTimer())
        {
            SetBool("isEating", true);
        }
        
        if (_playerDistance < _aiConfig.ChickenRunRange)
        {
            SetBool("isRunning", true);
        }
    }
}
