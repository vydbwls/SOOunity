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

    AudioSource Audio; //AudioSorce 컴포넌트를 변수로 담습니다.
    public static soundManager instance;  //자기자신을 변수로 담습니다.
    void Awake() //Start보다도 먼저, 객체가 생성될때 호출됩니다
    {
        if (soundManager.instance == null) //incetance가 비어있는지 검사합니다.
        {
            soundManager.instance = this; //자기자신을 담습니다.
        }
    }
    void Start()
    {
        Audio = this.gameObject.GetComponent<AudioSource>(); //AudioSource 오브젝트를 변수로 담습니다.
    }
    public void PlaySoundSFX()
    {
        Audio.PlayOneShot(SfxClips[SfxNum]); //soundExplosion을 재생합니다.
        SfxNum += 1;
    }
    public void PlaySoundBGM()
    {
        Audio.clip = BgmClips[BgmNum];  //soundExplosion을 재생합니다.
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
