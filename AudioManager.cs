using UnityEngine;
using UnityEngine.Audio; 

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] music;
    public AudioSource[] sfx;
    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup;  
    public AudioMixer audioMixer;

    private void Awake()
    {
        instance = this;

        foreach (AudioSource m in music)
        {
            m.outputAudioMixerGroup = musicMixerGroup;
        }

        foreach (AudioSource s in sfx)
        {
            s.outputAudioMixerGroup = sfxMixerGroup;
        }
    }

    public void PlayMusic(int musicToPlay)
    {
        if (musicToPlay >= 0 && musicToPlay < music.Length)
        {
            StopAllMusic(); 
            music[musicToPlay].Play();
        }
    }

    public void PlaySFX(int sfxToPlay)
    {
        if (sfxToPlay >= 0 && sfxToPlay < sfx.Length)
        {
            sfx[sfxToPlay].Play();
        }
    }
    public void StopAllMusic()
    {
        foreach (AudioSource m in music)
        {
            m.Stop();
        }
    }
    public void PauseMusic()
    {
        foreach (AudioSource m in music)
        {
            if (m.isPlaying)
            {
                m.Pause();
            }
        }
    }
    public void ResumeMusic()
    {
        foreach (AudioSource m in music)
        {
            if (!m.isPlaying)
            {
                m.UnPause();
            }
        }
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", PauseMenu.instance.musicSlider.value);
    }
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", PauseMenu.instance.sfxSlider.value);
    }
}
