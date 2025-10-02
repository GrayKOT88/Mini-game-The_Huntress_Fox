using UnityEngine;
using UnityEngine.AI;

public class RunChicken : StateMachineBehaviour
{
    float runRange = 10;    
    Transform player;
    NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(2f, 4.5f);
        //Debug.Log(agent.speed);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        Vector3 runDirection = animator.transform.position - player.position; // Вычисляем направление для убегания
        agent.SetDestination(animator.transform.position + runDirection); // Устанавливаем цель для навигации
        float distance = Vector3.Distance(animator.transform.position, player.position); //Дистанция между        
        if (distance > runRange)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        agent.speed = 1;
    }
}
