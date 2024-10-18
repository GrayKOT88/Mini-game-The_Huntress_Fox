using UnityEngine;

public class IdleChicken : StateMachineBehaviour
{
    float timer;
    float runRange = 5;
    Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > 3)
        {
            animator.SetBool("isEating", true);
        }
        float distance = Vector3.Distance(animator.transform.position, player.position); //Дистанция между
        if (distance < runRange)
        {
            animator.SetBool("isRunning", true);
        }
    }
}
