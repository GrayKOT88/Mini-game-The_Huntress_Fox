using UnityEngine;

public class WalkChicken : AIStateBase
{        
    private float _timeToWalking = 10f;    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        SetRandomDestination("PointsChicken");        
        SetRandomDuration(_timeToWalking, _timeToWalking + 21);
        _agent.speed = 1;
    }
        
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            SetRandomDestination("PointsChicken");            
        }
        
        if (UpdateStateTimer())
        {
            SetBool("isWalking", false);
        }
        
        if (_playerDistance < _runRange)
        {
            SetBool("isRunning", true);
        }
    }
        
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_agent.transform.position);
    }
}
