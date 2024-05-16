using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSound : MonoBehaviour
{
    private AudioSource dogVoice;
    public AudioClip barking;
    
    private void Start()
    {
        dogVoice = GetComponent<AudioSource>();
    }

    public void Bark()
    {
        dogVoice.clip = barking;
        dogVoice.Play();
    }
}
