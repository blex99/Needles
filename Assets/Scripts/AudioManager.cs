using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Sound[] music;

    public AudioMixer mixer;
    public AudioMixerGroup masterOut, musicOut, sfxOut;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = sfxOut;
            s.source.clip = s.clip; 
            s.source.volume = s.volume; 
            s.source.pitch = s.pitch; 
            s.source.loop = s.loop;
        }

        foreach (Sound m in music)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.outputAudioMixerGroup = musicOut;
            m.source.clip = m.clip; 
            m.source.volume = m.volume; 
            m.source.pitch = m.pitch; 
            m.source.loop = m.loop;
        }
    }

    private void Start()
    {
        mixer.SetFloat("masterVolume", Mathf.Log10(PlayerPrefs.GetFloat("masterVolume")) * 20);
        mixer.SetFloat("musicVolume", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        mixer.SetFloat("sfxVolume", Mathf.Log10(PlayerPrefs.GetFloat("sfxVolume")) * 20);
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Could not find sound name: " + name);
            return;
        }
        s.source.Play();
    }

    public void PlayMusic(string name)
    {
        bool alreadyPlaying = false;
        Sound m = Array.Find(music, music => music.name == name);

        // don't play song if it already is playing
        // and stop all other songs
        foreach (Sound i in music){
            if (i.source.isPlaying)
            {
                if (i.name == name) alreadyPlaying = true;
                else i.source.Stop();
            }
        }

        if (m == null)
        {
            Debug.LogWarning("Could not find song name: " + name);
            return;
        }

        if (!alreadyPlaying) m.source.Play();
    }

    public void StopAllMusic()
    {
        foreach (Sound m in music){
            m.source.Stop();
        }
    }
}
