using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake() 
    {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound not found!: " + name);
            return;
        }
        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound not found!: " + name);
            return;
        }
        s.source.Stop();
    }

    public void RandomizePitch(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound not found!: " + name);
            return;
        }
        s.source.pitch = UnityEngine.Random.Range(0f,1f);
    }

    public void PutPitch(string name, float pitchValue)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound not found!: " + name);
            return;
        }
        s.source.pitch = pitchValue;
    }

    public void StopAll()
    {
        for(int x = 0; x < sounds.Length; x++)
        {
            sounds[x].source.Stop();
        }
    }

    public IEnumerator QuitVolumeSlow(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = 0.3f;
        yield return new WaitForSecondsRealtime(0.3f);
        s.source.volume = 0.2f;
        yield return new WaitForSecondsRealtime(0.2f);
        s.source.volume = 0.1f;
        yield return new WaitForSecondsRealtime(0.1f);
        StopAll();
        s.source.volume = 0.4f;
    }
}
