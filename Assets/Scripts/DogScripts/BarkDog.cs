using UnityEngine;

public class BarkDog : StateMachineBehaviour
{
    Transform player;
    AudioSource playerAudio;    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerAudio = animator.GetComponent<AudioSource>();
        playerAudio.Play();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        animator.transform.LookAt(player.position);
        float distance = Vector3.Distance(animator.transform.position, player.position); //Дистанция между
        if (distance > 3)
        {            
            animator.SetBool("isBarking", false);            
        }
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerAudio.Stop();
    }
}
