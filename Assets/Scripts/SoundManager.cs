using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    SoundHolder soundHolder;
    Queue<AudioSource> destroy=new Queue<AudioSource>();
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
        BushCollect,
        Angle
    }
    private static Dictionary<Sound, float> soundplusTimer;

    public void PlaySound(Sound sound)
    {
        if (temp==null)
        {
            temp = new GameObject("Sound");
            audioSource = temp.AddComponent<AudioSource>();
        }
        audioSource.PlayOneShot(GetAudioClip(sound));
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

}
