using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolDog : StateMachineBehaviour
{
    [SerializeField] string pointsTag;
    float timer;
    int timePatrolling;
    float chaseRange = 15;    
    Transform player;    
    NavMeshAgent agent;
    List<Transform> points = new List<Transform>();

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;        
        agent = animator.GetComponent<NavMeshAgent>();
        Transform pointsOdject = GameObject.FindGameObjectWithTag(pointsTag).transform;
        foreach (Transform t in pointsOdject)
        {
            points.Add(t);
        }
        agent.SetDestination(points[Random.Range(0, points.Count)].position); //первая точка
        timePatrolling = Random.Range(30, 180);        
        //Debug.Log("time patrol  " + timePatrolling);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {      
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(points[Random.Range(0, points.Count)].position);
        }
        timer += Time.deltaTime;       
        if (timer > timePatrolling)
        {
            animator.SetBool("isPatrolling", false);           
        }
        float distance = Vector3.Distance(animator.transform.position, player.position); //Дистанция между
        if (distance < chaseRange)
        {
            animator.SetBool("isChasing", true);            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
}
