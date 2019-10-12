using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    //this script manages the sounds
    public static SoundManager Instance {get; private set;}
    public AudioSource audioSource;
    private void Awake(){
        Instance = this;
    }
    private void Start(){
        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(){
        audioSource.Play();
    }
    public void ChangeAudioClipAndPlay(AudioClip audioClip){
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public void ChangeWithSoundInfoAndPlay(SoundInfo soundInfo){
        audioSource.clip = soundInfo.clip;
        audioSource.Play();
    }
}
