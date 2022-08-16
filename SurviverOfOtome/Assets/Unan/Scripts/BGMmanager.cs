using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMmanager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip[] clips;
    public int ibgm;
    // Start is called before the first frame update
    void Start()
    {
       audioSource = GetComponent<AudioSource>();
       Play();
       //audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //audioSource.clip = clips[ibgm];
    }

    void Play()
    {
        audioSource.clip = clips[ibgm];
        audioSource.Play();
    }

    void Stop()
    {
        audioSource.Stop();
    }
}
