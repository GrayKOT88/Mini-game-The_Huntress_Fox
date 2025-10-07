using UnityEngine;

public class BarkDog : AIStateBase
{    
    AudioSource playerAudio;
    private float _barkingDistance = 3f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Initialize(animator);
        playerAudio = animator.GetComponent<AudioSource>();
        if (playerAudio != null)        
            playerAudio.Play();
        
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        _animator.transform.LookAt(_player.position);         
        if (_playerDistance > _barkingDistance)
        {            
            SetBool("isBarking", false);            
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (playerAudio != null)
            playerAudio.Stop();
    }
}
