using UnityEngine;

public class SittingDog : StateMachineBehaviour
{
    float timer;
    float chaseRange = 15;
    int timeSitting;
    Transform player;    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeSitting = Random.Range(5,21);
        //Debug.Log("timeSitting  " +  timeSitting);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {       
        timer += Time.deltaTime;                
        if (timer > timeSitting)
        {            
            animator.SetBool("Sit_b", false);            
            animator.SetBool("isPatrolling", true);            
        }
        float distance = Vector3.Distance(animator.transform.position, player.position); //Дистанция между
        if (distance < chaseRange)
        {
            animator.SetBool("Sit_b", false);
            animator.SetBool("isChasing", true);
        }
    }
}
