using UnityEngine;

public class EatChicken : AIStateBase
{
    private float _timeToEating = 5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        SetRandomDuration(_timeToEating, _timeToEating + 25);        
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        if (UpdateStateTimer())
        {
            SetBool("isEating", false);
            SetBool("isWalking", true);
        }
        
        if (_playerDistance < 5)
        {
            SetBool("isEating", false);
        }
    }
}
