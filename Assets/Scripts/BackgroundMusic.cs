using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic Instance;

    public AudioClip music;
    [Range(0f, 1f)] public float volume = 0.5f;
    public bool loop = true;
    public AudioSource source;


    void Start()
    {
        source.playOnAwake = false;
        source.loop = loop;
        source.volume = volume;
        source.spatialBlend = 0f; // 2D sound
        if (music != null) source.clip = music;
        if (music != null) source.Play();
    }

    public void SetVolume(float v) { volume = Mathf.Clamp01(v); source.volume = volume; }
    public void Play() { if (!source.isPlaying && source.clip != null) source.Play(); }
    public void Pause() { if (source.isPlaying) source.Pause(); }
    public void Stop() { source.Stop(); }
    public void SetClip(AudioClip clip, bool autoPlay = true)
    {
        source.clip = clip;
        if (autoPlay && clip != null) Play();
    }
}