using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BGM_T
{
    public string name;
    public AudioClip clip;
};

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;

    [SerializeField] BGM[] BGM = null;


    private AudioSource audioSource;

    public string bgmName;
    // Start is called before the first frame update
    void Start()
    {
       audioSource = GetComponent<AudioSource>();
        Play();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Play()
    {
        for(int i = 0; i < BGM.Length; i++)
        {
            if(bgmName == BGM[i].name)
            {
                audioSource.clip = BGM[i].clip;
                audioSource.Play();
            }
        }
    }

    void Stop()
    {
        audioSource.Stop();
    }
}
