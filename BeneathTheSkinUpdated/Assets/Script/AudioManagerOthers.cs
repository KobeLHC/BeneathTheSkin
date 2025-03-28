using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerOthers : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip background;
    public AudioClip oneTimeEffectSound;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        musicSource.loop = true;
        //Debug.Log("Played music");
        PlaySFX(oneTimeEffectSound);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
        //Debug.Log("SFX played");
    }

    //public void LoopSFX(AudioClip clip)
    //{
    //    if (SFXSource.clip != clip)
    //    {
    //        SFXSource.clip = clip;
    //        SFXSource.loop = true;
    //        SFXSource.Play();
    //        //Debug.Log("Looping SFX started");
    //    }
    //}

    //public void EndSFX(AudioClip clip)
    //{
    //    if (SFXSource.clip == clip)
    //    {
    //        SFXSource.Stop();
    //        SFXSource.loop = false;
    //        SFXSource.clip = null;
    //        //Debug.Log("Looping SFX stopped");
    //    }
    //}
}
