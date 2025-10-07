using System.Collections;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip chickenSound;
    private AudioSource audioSource;
    private bool isPlaying = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayChickenSound()
    {
        if(!isPlaying)
        {
            isPlaying = true;
            audioSource.PlayOneShot(chickenSound, 2f);
            StartCoroutine(ResetPlayingFlag(chickenSound.length));
        }
    }

    private IEnumerator ResetPlayingFlag(float duration)
    {
        yield return new WaitForSeconds(duration);
        isPlaying = false;
    }
}