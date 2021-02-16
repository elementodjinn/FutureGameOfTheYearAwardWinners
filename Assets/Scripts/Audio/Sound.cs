using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Sound
{
    public string name;

    //Base of Sound Class
    public AudioClip clip;
    public AudioMixer group;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;

    public bool loop;
    public bool playOnAwake;
}
