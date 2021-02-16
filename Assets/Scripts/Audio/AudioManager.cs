using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    //Array of Sound objects (Audio) to be used throughout the game
    public Sound[] sounds;

    //Audio Source to play sfx sounds.
    public AudioSource sfx;

    //Accessing class across entirety of game.
    public static AudioManager instance;

    // Start is called before the first frame update
    void Start()
    {
        //Variation of background music can go here.
    }

    
    void Awake()
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

        //Each Sound Clip gets appropraitly created
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            //s.source.group = s.group; Having trouble getting this assigned, might be wrong.
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
