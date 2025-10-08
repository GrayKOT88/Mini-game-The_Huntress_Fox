using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIStateBase : StateMachineBehaviour
{
    protected AIConfig _aiConfig => AIConfig.Instance; // Автоматическая загрузка
    protected Transform _player;
    protected NavMeshAgent _agent;
    protected Animator _animator;
    protected float _timer;
    protected float _randomDuration;
    
    protected float _playerDistance => Vector3.Distance(_animator.transform.position, _player.position);
    private static Transform _cachedPlayer; // Статическое кэширование игрока

    private static readonly Dictionary<string, List<Transform>> _cachedPoints = new Dictionary
        <string, List<Transform>>();

    public virtual void Initialize(Animator animator)
    {
        _animator = animator;
        if(_cachedPlayer == null)
            _cachedPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        _player = _cachedPlayer;
        _agent = animator.GetComponent<NavMeshAgent>();
        _timer = 0;        
    }

    protected List<Transform> GetPointsByTag(string tag)
    {
        // Если точки для этого тега уже закэшированы - возвращаем их
        if (_cachedPoints.ContainsKey(tag) && _cachedPoints[tag] != null && _cachedPoints[tag].Count > 0)
        {
            return _cachedPoints[tag];
        }

        // Иначе ищем и кэшируем точки
        _cachedPoints[tag] = new List<Transform>();
        GameObject pointsObject = GameObject.FindGameObjectWithTag(tag);

        if (pointsObject != null)
        {
            foreach (Transform child in pointsObject.transform)
            {
                _cachedPoints[tag].Add(child);
            }
        }
        else
        {
            Debug.LogWarning($"No object found with tag: {tag}");
        }

        return _cachedPoints[tag];
    }

    protected Transform GetRandomPoint(string tag)
    {
        var points = GetPointsByTag(tag);
        return points.Count > 0 ? points[Random.Range(0, points.Count)] : null;
    }

    protected void SetRandomDestination(string tag)
    {
        Transform randomPoint = GetRandomPoint(tag);
        if (randomPoint != null)
        {
            _agent.SetDestination(randomPoint.position);            
        }       
    }

    protected virtual bool UpdateStateTimer()
    {
        _timer += Time.deltaTime;
        return _randomDuration > 0 && _timer >= _randomDuration;
    }

    protected void SetRandomDuration(float min, float max)
    {
        _randomDuration = Random.Range(min, max);
        _timer = 0f;
    }

    protected void SetBool(string param, bool value) => _animator.SetBool(param, value);
    protected void SetFloat(string param, float value) => _animator.SetFloat(param, value);
}
