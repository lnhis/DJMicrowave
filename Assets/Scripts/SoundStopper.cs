using UnityEngine;
using System.Collections;

public class SoundStopper : MonoBehaviour
{
    private AudioSource ass;
    public bool MuteMusic { get; set; }

    public float SoundStartDelay = -1f;
    public float SoundEndDuration = -1f;

    private float soundStartTime = 0f;
    private float soundPlayStartTime = 0f;

    private float utmostLimit = 20f;

    private bool startedPlaying = false;

    void Awake()
    {
        ass = this.GetComponent<AudioSource>();
        soundStartTime = Time.time;
    }
	
	void Update ()
    {
        if(!startedPlaying && Time.time - soundStartTime > SoundStartDelay)
        {
            ass.Play();
            soundPlayStartTime = Time.time;
            startedPlaying = true;
        }
        if(MuteMusic)
        {
            SoundSystem.Instance.MuteMusic = true;
        }
	    if(startedPlaying && !ass.isPlaying)
        {
            SoundSystem.Instance.MuteMusic = false;
            SoundSystem.Instance.RemoveSound(ass.GetInstanceID());
        }
        else if(startedPlaying && SoundEndDuration > 0f)
        {
            if(Time.time - soundPlayStartTime > SoundEndDuration)
            {
                SoundSystem.Instance.MuteMusic = false;
                SoundSystem.Instance.RemoveSound(ass.GetInstanceID());
            }
        }

        if(startedPlaying && Time.time - soundStartTime > utmostLimit)
        {
            SoundSystem.Instance.MuteMusic = false;
            SoundSystem.Instance.RemoveSound(ass.GetInstanceID());
        }
	}
}
