using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [System.Serializable]
    public struct Sound
    {
        public AudioClip sound;
        public string soundString;

        public Sound(AudioClip _sound, string _soundString)
        {
            sound = _sound;
            soundString = _soundString;
        }
    }

    public AudioSource source;
    public Sound[] sounds;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play(string soundString)
    {
        foreach(Sound s in sounds)
        {
            if (s.soundString == soundString)
            {
                source.clip = s.sound;
                source.Play();
            }
        }
    }

    public bool IsPlaying()
    {
        if (source.isPlaying)
            return true;
        else
            return false;
    }
}
