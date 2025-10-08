using UnityEngine;

public class EatChicken : AIStateBase
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        SetRandomDuration(_aiConfig.ChickenEatTime, _aiConfig.ChickenEatTime + 25);        
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        if (UpdateStateTimer())
        {
            SetBool("isEating", false);
            SetBool("isWalking", true);
        }
        
        if (_playerDistance < _aiConfig.ChickenRunRange)
        {
            SetBool("isEating", false);
        }
    }
}
