using UnityEngine;

public class RunChicken : AIStateBase
{
    float runRange = 10;    
        
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);        
        _agent.speed = Random.Range(3.8f, 5.1f);        
    }
        
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 runDirection = animator.transform.position - _player.position; // ��������� ����������� ��� ��������
        _agent.SetDestination(animator.transform.position + runDirection); // ������������� ���� ��� ���������
                
        if (_playerDistance > runRange)
        {
            SetBool("isRunning", false);
            SetBool("isWalking", false);
        }        
    }
        
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_agent.transform.position);
        _agent.speed = 1;
    }
}
