using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Controla el sonido del juego
 */
public class AudioManager : MonoBehaviour
{

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    

    public void PlayAudioClip(GameObject sender, object data)
    {
        if(data is  AudioClip clip) 
        {
            audioSource.PlayOneShot(clip);
        }
        
    }

    public void StopAudioClip()
    {
        audioSource.Stop();
    }
}
