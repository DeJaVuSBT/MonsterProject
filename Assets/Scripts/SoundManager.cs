using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    public enum Sound{
        PlayerMove,
        Interact,
        MoralityBarIncrease,
        MoralityBarDecrease,
        VillagerEmotionReact,
        BackGroundMusic
    }
    private static Dictionary<Sound, float> soundplusTimer;

    public static void PlaySound(Sound sound) {
        if (CanPlay(sound))
        {
            GameObject soundsObj = new GameObject("Sounds");
            AudioSource audioSource = soundsObj.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }
    }
    public static void PlaySound(Sound sound, Vector3 pos) {
        if (CanPlay(sound))
        {
            GameObject soundsObj = new GameObject("Sounds");
            soundsObj.transform.position = pos;
            AudioSource audioSource = soundsObj.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.Play();
        }
    }
    private static bool CanPlay(Sound sound) {

        if (IfWithTimer(sound))
        {
            if (soundplusTimer.ContainsKey(sound))
            {
                float lastTimePlayed = soundplusTimer[sound];
                float TimerInterval = 0.1f;
                if (lastTimePlayed + TimerInterval < Time.time)
                {
                    soundplusTimer[sound]=Time.time;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
            
        }
        else
        {
            return true;
        }
    }
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundHolder.SoundAudioClip clip in SoundHolder.instance.soundAudioClips) {
            if (clip.sound==sound)
            {
                return clip.clip;
            }
        }
        Debug.LogError("Sound" + sound + "not found~");
        return null;
    }
    private static bool IfWithTimer (Sound sound)
    {
        foreach (SoundHolder.SoundAudioClip clip in SoundHolder.instance.soundAudioClips)
        {
            if (clip.sound == sound)
            {
                return clip.withTimer;
            }
        }
        Debug.LogError("Sound" + sound + "not found~");
        return false;
    }
}
