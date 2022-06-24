using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    SoundHolder soundHolder;
    GameObject temp;
    AudioSource audioSource;
    public enum Sound
    {
        TreeShake,
        TreeHit,
        TreeFall,
        Hunger50,
        Hunger25,
        Hunger0,
        Demon,
        BushHit,
        BushShake,
        Angle,
        Eat,
        Walking,
        Runing,
        CageHit,
        LogDrag,
        RockDrag
    }
    private Dictionary<Sound, float> soundWithTimer;

    private void Awake()
    {
        soundWithTimer = new Dictionary<Sound, float>();
        soundWithTimer[Sound.Walking] = 0f;
        soundWithTimer[Sound.Runing] = 0f;
    }
    public void PlaySound(Sound sound)
    {
        if (CanPlay(sound))
        {
            if (temp == null)
            {
                temp = new GameObject("Sound");
                audioSource = temp.AddComponent<AudioSource>();
            }
            audioSource.volume = GetAudioVolum(sound);
            if (IfIsLoop(sound))
            {
                audioSource.loop=true;
                audioSource.clip = GetAudioClip(sound);
                audioSource.Play();
            }
            else
            {
                audioSource.loop = false;
                audioSource.PlayOneShot(GetAudioClip(sound));
            }
           
        }

    }

    public void StopSound()
    {
        audioSource.Stop();
    }
    private bool CanPlay(Sound sound) {
        switch (sound) {
            default:
                return true;
            case Sound.Walking:
                if (soundWithTimer.ContainsKey(sound))
                {
                    float prePlay = soundWithTimer[sound];
                    float timeGap = 0.7f;
                    if (prePlay + timeGap<Time.time)
                    {
                        soundWithTimer[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            case Sound.Runing:
                if (soundWithTimer.ContainsKey(sound))
                {
                    float prePlay = soundWithTimer[sound];
                    float timeGap = 0.55f;
                    if (prePlay + timeGap < Time.time)
                    {
                        soundWithTimer[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            

        }
    
    }

    private AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundHolder.SoundAudioClip clip in soundHolder.soundAudioClips)
        {
            if (clip.sound == sound)
            {
                return clip.clip();
            }
        }
        Debug.LogError("Sound" + sound + "not found~");
        return null;
    }
    private float GetAudioVolum(Sound sound)
    {
        foreach (SoundHolder.SoundAudioClip clip in soundHolder.soundAudioClips)
        {
            if (clip.sound == sound)
            {
                return clip.volum;
            }
        }
        Debug.LogError("Sound" + sound + "not found~");
        return 1;
    }
    private bool IfIsLoop(Sound sound)
    {
        foreach (SoundHolder.SoundAudioClip clip in soundHolder.soundAudioClips)
        {
            if (clip.sound == sound)
            {
                return clip.loop;
            }
        }
        Debug.LogError("Sound" + sound + "not found~");
        return false;
    }

}
