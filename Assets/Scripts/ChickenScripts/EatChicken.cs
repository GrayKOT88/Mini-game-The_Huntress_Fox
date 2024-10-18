using UnityEngine;

public class EatChicken : StateMachineBehaviour
{
    float timer;
    int timeEating;
    Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeEating = Random.Range(3, 21);
        //Debug.Log("timeEating  " + timeEating);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > timeEating)
        {
            animator.SetBool("isEating", false);
            animator.SetBool("isWalking", true);
        }
        float distance = Vector3.Distance(animator.transform.position, player.position); //Дистанция между
        if (distance < 5)
        {
            animator.SetBool("isEating", false);
        }
    }
}
