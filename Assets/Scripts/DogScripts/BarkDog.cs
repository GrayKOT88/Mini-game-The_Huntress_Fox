using UnityEngine;

public class BarkDog : AIStateBase
{    
    AudioSource playerAudio;    
        
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        playerAudio = animator.GetComponent<AudioSource>();
        playerAudio.Play();        
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        _animator.transform.LookAt(_player.position);         
        if (_playerDistance > 3)
        {            
            SetBool("isBarking", false);            
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerAudio.Stop();
    }
}
