using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioMenu : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;

    public AudioClip menuGameMusicClip;

    private void Start()
    {
        musicAudioSource.clip = menuGameMusicClip;
        musicAudioSource.Play();
    }
    public void PlayMusic(AudioClip sfxClip)
    {
        vfxAudioSource.clip = sfxClip;
        vfxAudioSource.Play();
    }
/*    public void SetVolume(float volume)
    {
        vfxAudioSource.volume = volume;  // Giá trị volume từ 0.0 đến 1.0
    }*/
}
