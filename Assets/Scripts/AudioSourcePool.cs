using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    public AudioSource AudioSourcePrefab = null;

    public static AudioSourcePool instance;

    private List<AudioSource> AudioSources = new List<AudioSource>();

    private void Awake()
    {
        instance = this;
    }

    public AudioSource GetSource()
    {
        foreach(AudioSource audiosource in AudioSources)
        {
            if(!audiosource.isPlaying)
            {
                return audiosource;
            }
        }
        var newSource = Instantiate<AudioSource>(AudioSourcePrefab);
        newSource.transform.SetParent(transform, false);
        AudioSources.Add(newSource);
        return newSource;
    }
}
