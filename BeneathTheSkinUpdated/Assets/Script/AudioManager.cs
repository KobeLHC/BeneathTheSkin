using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip background;
    public AudioClip clicking;
    public AudioClip walking;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        Debug.Log("Played music");
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
        Debug.Log("SFX played");
    }

    public void LoopSFX(AudioClip clip)
    {
        if (SFXSource.clip != clip)
        {
            SFXSource.clip = clip;
            SFXSource.loop = true;
            SFXSource.Play();
            Debug.Log("Looping SFX started");
        }
    }

    public void EndSFX(AudioClip clip)
    {
        if (SFXSource.clip == clip)
        {
            SFXSource.Stop();
            SFXSource.loop = false;
            SFXSource.clip = null;
            Debug.Log("Looping SFX stopped");
        }
    }
}
