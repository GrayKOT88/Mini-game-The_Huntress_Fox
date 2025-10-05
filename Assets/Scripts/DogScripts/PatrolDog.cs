using System.Collections.Generic;
using UnityEngine;

public class PatrolDog : AIStateBase
{
    [SerializeField] string pointsTag;    
    float chaseRange = 15;
    private float _timeToPatrol = 30f;
    List<Transform> points = new List<Transform>();
        
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);        
        Transform pointsOdject = GameObject.FindGameObjectWithTag(pointsTag).transform;
        foreach (Transform t in pointsOdject)
        {
            points.Add(t);
        }
        _agent.SetDestination(points[Random.Range(0, points.Count)].position); //первая точка
        SetRandomDuration(_timeToPatrol, _timeToPatrol + 150);       
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {      
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _agent.SetDestination(points[Random.Range(0, points.Count)].position);
        }
               
        if (UpdateStateTimer())
        {
            SetBool("isPatrolling", false);           
        }
        
        if (_playerDistance < chaseRange)
        {
            SetBool("isChasing", true);            
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_agent.transform.position);
    }
}
