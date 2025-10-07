using System.Collections.Generic;
using UnityEngine;

public class WalkChicken : AIStateBase
{        
    private float _timeToWalking = 10f;
    private static List<Transform> points = new List<Transform>(); // статическое кэширование
    private static bool pointsCached = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);

        if (!pointsCached) // Кэшируем точки один раз для всех кур
        {
            points = new List<Transform>();
            Transform pointsOdject = GameObject.FindGameObjectWithTag("PointsChicken").transform;
            foreach (Transform t in pointsOdject)
            {
                points.Add(t);
            }
            pointsCached = true;
        }

        if(points.Count > 0)
        {
            _agent.SetDestination(points[Random.Range(0, points.Count)].position); //первая точка
        }
                                                                               
        SetRandomDuration(_timeToWalking, _timeToWalking + 21);
        _agent.speed = 1;
    }
        
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _agent.SetDestination(points[Random.Range(0, points.Count)].position);
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
