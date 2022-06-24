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
       // public AudioClip clip;
        public AudioClip clip (){  return a [Random.Range(0, a.Length)]; }
        public AudioClip[] a;
        [Range(0,1)]
        public float volum=1;
        public bool loop;
    }


}
