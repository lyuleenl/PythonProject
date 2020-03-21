using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManager {
    public AudioManager(GameFacade facade) : base(facade) { }

    private const string Path_Perfix = "Sounds/";
    public const string Sound_Alert = "Alert";
    public const string Sound_ArrowShoot = "ArrowShoot";
    public const string Sound_Bgfast = "Bg(fast)";
    public const string Sound_Bgmoderate = "bgm";
    public const string Sound_ButtonClick = "ButtonClick";
    public const string Sound_Miss = "Miss";
    public const string Sound_ShootPerson = "ShootPerson";
    public const string Sound_Timer = "Timer";

    private AudioSource bgAudioSource;
    private AudioSource normalAudioSource;


    public override void OnInit()
    {
        GameObject audioSourceGO = new GameObject("AudioSourceGo");
        bgAudioSource = audioSourceGO.AddComponent<AudioSource>();
        normalAudioSource = audioSourceGO.AddComponent<AudioSource>();

        PlayAudio(bgAudioSource,LoadClip(Sound_Bgmoderate),true,0.2f);
    }
    //public void PlayBGM(string soundName)
    //{
    //    PlayAudio(bgAudioSource, LoadClip(soundName), true, 0.5f);
    //}
    public void PlayNormalAudio(string soundName)
    {
        PlayAudio(normalAudioSource, LoadClip(soundName), false,1f);
    }
    private  void PlayAudio(AudioSource audioSource, AudioClip audioClip,bool isLoop,float volume)
    {
        audioSource.clip = audioClip;
        audioSource.loop = isLoop;
        audioSource.volume = volume;
        audioSource.Play();
    }
    private AudioClip LoadClip(string soundName)
    {
        return Resources.Load<AudioClip>(Path_Perfix + soundName);
    }
}
