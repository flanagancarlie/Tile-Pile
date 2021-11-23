using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour{

    public Sound[] sounds;
    public static float volume;


    void Awake()
    {

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        
    }

    public void setVolume(float volume)
    {
        foreach (Sound s in sounds)
        {

            s.source.volume = volume;
        }
        AudioManager.volume = volume;
        Debug.Log(AudioManager.volume);
    }

    void Start () {
        this.setVolume(volume);
        Play("BackgroundMusic");
    }

    public void Play (string name) {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(sounds == null) 
         {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

}
