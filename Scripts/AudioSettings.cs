using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    public float effectPercent = 0.5f;
    public float musicPercent = 0.5f;

    public void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("MusicBox");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetEffect(float percent)
    {
        effectPercent = percent;
    }

    public void SetMusic(float percent)
    {
        musicPercent = percent;
    }

    public void UpdateEffect()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        foreach(AudioSource source in sources)
        {
            if (source.gameObject.tag != "MusicBox" && source.volume != effectPercent)
                source.volume = effectPercent;
        }
    }

    public void UpdateMusic()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("MusicBox");
        obj.GetComponent<AudioSource>().volume = musicPercent;
    }
}
