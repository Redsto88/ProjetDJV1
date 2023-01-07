using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

[SingletonOptions("AUDIO", isPrefab: false)]
public class AudioManager : Singleton<AudioManager>
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;

        [Range(0.1f, 3f)]
        public float pitch;

        [System.NonSerialized]
        public AudioSource source;

        public bool loop;
    }

    public Sound[] sounds;

    new public void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }


    

    public void Play (string name, float volume, float pitch = 1f)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            return;
        }
        s.source.pitch = pitch;
        s.source.volume = volume;
        s.source.Play();

    }

    public void Stop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null){
            return;
        }

        s.source.Stop();

    }

    public void Start()
    {
        Play("Ambiance",0.8f);
    }

    public void setPitch(string name, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            return;
        }

        s.source.pitch = pitch;
    }

}
