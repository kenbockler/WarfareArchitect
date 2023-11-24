using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="AudioClipGroup")]
public class AudioClipGroup : ScriptableObject
{
    // https://freesound.org/people/Breviceps/sounds/505407/
    // https://freesound.org/people/peridactyloptrix/sounds/216909/
    // https://freesound.org/people/InspectorJ/sounds/400991/
    // https://freesound.org/people/tosha73/sounds/495309/
    public float VolumeMin = 1f;
    public float VolumeMax = 1f;
    public float PitchMin = 1f;
    public float PitchMax = 1f;

    public float Cooldown = 0.2f;

    public List<AudioClip> AudioClips;

    private float nextPlayTime = 0f;

    public void Start()
    {
        nextPlayTime = 0f;
    }

    public void Play(float pitch = 0/*, Vector3? position = null*/)
    {
        Play(AudioSourcePool.instance.GetSource()/*, pitch, position*/);
    }

    public void Play(AudioSource source/*, float pitch = 0, Vector3? position = null*/)
    {
        if(Time.time < nextPlayTime){
            if(nextPlayTime - Time.time > Cooldown) nextPlayTime = Time.time;
            return;
        }
        /*
        if(position != null)
        {
            source.spatialBlend = 0f;
            source.transform.position = position ?? Vector3.zero;
        }
        else
        {
            source.spatialBlend = 1f;
        }*/

        source.clip = AudioClips[Random.Range(0, AudioClips.Count)];
        source.volume = Random.Range(VolumeMin, VolumeMax);
        source.transform.position = source.transform.parent.transform.position;
        source.Play();
        nextPlayTime = Time.time + Cooldown;
    }

    public void Play(Vector3 location, float pitch = 0)
    {
        Play(AudioSourcePool.instance.GetSource(), location);
    }

    public void Play(AudioSource source, Vector3 location)
    {
        if(Time.time < nextPlayTime){
            if(nextPlayTime - Time.time > Cooldown) nextPlayTime = Time.time;
            return;
        }
        /*
        if(position != null)
        {
            source.spatialBlend = 0f;
            source.transform.position = position ?? Vector3.zero;
        }
        else
        {
            source.spatialBlend = 1f;
        }*/

        source.clip = AudioClips[Random.Range(0, AudioClips.Count)];
        source.volume = Random.Range(VolumeMin, VolumeMax);
        source.pitch = Random.Range(PitchMin, PitchMax);
        Debug.Log(source.transform.parent.transform.position);
        Debug.Log(location);
        source.transform.position = location;
        source.Play();
        nextPlayTime = Time.time + Cooldown;
    }
}
