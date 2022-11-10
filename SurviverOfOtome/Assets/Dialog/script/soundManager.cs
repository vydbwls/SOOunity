using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BGM
{
    public string name;
    public AudioClip clip;
};

public class soundManager : MonoBehaviour
{
 //   [SerializeField] BGM[] BGM = null;
    public int SfxNum;
    public int BgmNum;
    public AudioClip[] BgmClips;
    public AudioClip[] SfxClips;

    AudioSource Audio; //AudioSorce ������Ʈ�� ������ ����ϴ�.
    public static soundManager instance;  //�ڱ��ڽ��� ������ ����ϴ�.
    void Awake() //Start���ٵ� ����, ��ü�� �����ɶ� ȣ��˴ϴ�
    {
        if (soundManager.instance == null) //incetance�� ����ִ��� �˻��մϴ�.
        {
            soundManager.instance = this; //�ڱ��ڽ��� ����ϴ�.
        }
    }
    void Start()
    {
        Audio = this.gameObject.GetComponent<AudioSource>(); //AudioSource ������Ʈ�� ������ ����ϴ�.
    }
    public void PlaySoundSFX()
    {
        Audio.PlayOneShot(SfxClips[SfxNum]); //soundExplosion�� ����մϴ�.
        SfxNum += 1;
    }
    public void PlaySoundBGM()
    {
        Audio.clip = BgmClips[BgmNum];  //soundExplosion�� ����մϴ�.
        Audio.Play();
        BgmNum += 1;
    }
    public void Stop()
    {
        Audio.Stop();
    }
    void Update()
    {

    }

}
