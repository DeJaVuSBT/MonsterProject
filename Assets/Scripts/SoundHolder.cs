using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundHolder : MonoBehaviour
{
    public static SoundHolder instance;
    public SoundAudioClip[] soundAudioClips;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip clip;
        public bool withTimer;
    }
}
