using UnityEngine;
using UnityEngine.AI;

public class ChaseDog : StateMachineBehaviour
{
    [SerializeField] float speedChase;
    float chaseRange = 20; //разстояние через которое не будет приследовать
    float attackRange = 1.5f;
    Transform player;    
    NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;        
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = speedChase;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);
        float distance = Vector3.Distance(animator.transform.position, player.position); //Дистанция между
        if (distance < attackRange)
        {
            animator.SetBool("isBarking", true);            
        }
        if (distance > chaseRange)
        {
            animator.SetBool("isChasing", false);                      
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
        agent.speed = 1;
    }
}
