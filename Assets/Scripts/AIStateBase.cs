using UnityEngine;
using UnityEngine.AI;

public abstract class AIStateBase : StateMachineBehaviour
{
    protected Transform _player;
    protected NavMeshAgent _agent;
    protected Animator _animator;

    protected float _timer;
    protected float _randomDuration;

    protected float _playerDistance => Vector3.Distance(_animator.transform.position, _player.position);

    public virtual void Initialize(Animator animator)
    {
        _animator = animator;
        if(_player == null)
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = animator.GetComponent<NavMeshAgent>();
        _timer = 0;
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
